import { useMemo } from "react";
import { Box, Button, Tooltip, IconButton } from "@mui/material";
import {
  MaterialReactTable,
  useMaterialReactTable,
} from "material-react-table";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useGetProducts } from "../hooks/useGetProducts";
import { useDeleteProduct } from "../hooks/useDeleteProduct";
import { useUpdateProduct } from "../hooks/useUpdateProduct";


const ProductList = () => {
  const { data: products = [], isLoading, isError } = useGetProducts();
  const { mutateAsync: deleteProduct } = useDeleteProduct();
  const { mutateAsync: updateProduct } = useUpdateProduct();

  const columns = useMemo(
    () => [
      {
        accessorKey: "id",
        header: "ID",
        size: 80,
      },
      {
        accessorKey: "title",
        header: "Title",
      },
      {
        accessorKey: "description",
        header: "Description",
      },
      {
        accessorKey: "price",
        header: "Price",
      },
      {
        accessorKey: "imageUrl",
        header: "Image URL",
      },
      {
        accessorKey: "category.title",
        header: "Category",
      },
    ],
    []
  );

  const handleDeleteProduct = async (id: string) => {
    if (window.confirm("Are you sure you want to delete this product?")) {
      await deleteProduct(id);
    }
  };

  const handleSaveProduct = async (values: any) => {
    await updateProduct(values);
  };

  const table = useMaterialReactTable({
    columns,
    data: products,
    enableEditing: true,
    onEditingRowSave: handleSaveProduct,
    renderRowActions: ({ row }) => (
      <Box sx={{ display: "flex", gap: "1rem" }}>
        <Tooltip title="Edit">
          <IconButton onClick={() => table.setEditingRow(row)}>
            <EditIcon />
          </IconButton>
        </Tooltip>
        <Tooltip title="Delete">
          <IconButton
            color="error"
            onClick={() => handleDeleteProduct(row.original.id)}
          >
            <DeleteIcon />
          </IconButton>
        </Tooltip>
      </Box>
    ),
    renderTopToolbarCustomActions: ({ table }) => (
      <Button variant="contained" onClick={() => table.setCreatingRow(true)}>
        Create New Product
      </Button>
    ),
    state: {
      isLoading,
      isSaving: false,
      showAlertBanner: isError,
      showProgressBars: false,
    },
  });

  return <MaterialReactTable table={table} />;
};

export default ProductList;
