import { useMemo, useState } from "react";
import {
  MRT_EditActionButtons,
  MaterialReactTable,
  type MRT_ColumnDef,
  type MRT_Row,
  type MRT_TableOptions,
  useMaterialReactTable,
} from "material-react-table";
import {
  Box,
  Button,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
  Tooltip,
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import EditCategoryForm from "./EditCategoryForm"; // Import EditCategoryForm
import { CategoryDtoInput } from "../types/types"; // Adjust import path if needed
import { useGetCategories } from "../hooks/useGetCategories";
import { useDeleteCategory } from "../hooks/useDeleteCategory";
import { useUpdateCategory } from "../hooks/useUpdateCategory";

const validateCategory = (category: CategoryDtoInput) => {
  return {
    title: !category.title.length ? "Title is required" : "",
  };
};

const CategoryList = () => {
  const [validationErrors, setValidationErrors] = useState<
    Record<string, string | undefined>
  >({});

  const columns = useMemo<MRT_ColumnDef<CategoryDtoInput>[]>(
    () => [
      {
        accessorKey: "id",
        header: "ID",
        enableEditing: false,
        size: 80,
      },
      {
        accessorKey: "title",
        header: "Title",
        muiEditTextFieldProps: {
          required: true,
          error: !!validationErrors?.title,
          helperText: validationErrors?.title,
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              title: undefined,
            }),
        },
      },
    ],
    [validationErrors]
  );

  const {
    data: fetchedCategories = [],
    isError: isLoadingCategoriesError,
    isFetching: isFetchingCategories,
    isLoading: isLoadingCategories,
  } = useGetCategories();
  const { mutateAsync: updateCategory, isPending: isUpdatingCategory } =
    useUpdateCategory();
  const { mutateAsync: deleteCategory, isPending: isDeletingCategory } =
    useDeleteCategory();

  const handleSaveCategory: MRT_TableOptions<CategoryDtoInput>["onEditingRowSave"] =
    async ({ values, table }) => {
      const newValidationErrors = validateCategory(values);
      if (Object.values(newValidationErrors).some((error) => error)) {
        setValidationErrors(newValidationErrors);
        return;
      }
      setValidationErrors({});
      await updateCategory(values);
      table.setEditingRow(null); // Exit editing mode
    };

  const openDeleteConfirmModal = (row: MRT_Row<CategoryDtoInput>) => {
    if (window.confirm("Are you sure you want to delete this category?")) {
      deleteCategory(row.original.id);
    }
  };

  const table = useMaterialReactTable({
    columns,
    data: fetchedCategories,
    editDisplayMode: "modal",
    enableEditing: true,
    getRowId: (row) => row.id,
    muiToolbarAlertBannerProps: isLoadingCategoriesError
      ? {
          color: "error",
          children: "Error loading data",
        }
      : undefined,
    muiTableContainerProps: {
      sx: {
        minHeight: "500px",
      },
    },
    onEditingRowCancel: () => setValidationErrors({}),
    onEditingRowSave: handleSaveCategory,
    // renderEditRowDialogContent: ({ table, row }) => (
    //   <>
    //     <DialogTitle variant="h3">Edit Category</DialogTitle>
    //     <DialogContent
    //       sx={{ display: "flex", flexDirection: "column", gap: "1.5rem" }}
    //     >
    //       <EditCategoryForm category={row.original} />{" "}
    //       {/* Use EditCategoryForm */}
    //     </DialogContent>
    //     <DialogActions>
    //       <Button onClick={() => table.setEditingRow(null)} color="inherit">
    //         Cancel
    //       </Button>
    //       <Button
    //         onClick={() => table.getEditingRowModel()?.submit()}
    //         variant="contained"
    //       >
    //         Save
    //       </Button>
    //     </DialogActions>
    //   </>
    // ),
    renderRowActions: ({ row, table }) => (
      <Box sx={{ display: "flex", gap: "1rem" }}>
        <Tooltip title="Edit">
          <IconButton onClick={() => table.setEditingRow(row)}>
            <EditIcon />
          </IconButton>
        </Tooltip>
        <Tooltip title="Delete">
          <IconButton color="error" onClick={() => openDeleteConfirmModal(row)}>
            <DeleteIcon />
          </IconButton>
        </Tooltip>
      </Box>
    ),
    renderTopToolbarCustomActions: () => (
      <Button variant="contained" onClick={() => table.setCreatingRow(true)}>
        Create New Category
      </Button>
    ),
    state: {
      isLoading: isLoadingCategories,
      isSaving: isUpdatingCategory || isDeletingCategory,
      showAlertBanner: isLoadingCategoriesError,
      showProgressBars: isFetchingCategories,
    },
  });

  return <MaterialReactTable table={table} />;
};

export default CategoryList;
