import { useForm } from "react-hook-form";
import { Box, Button, TextField, Select, MenuItem } from "@mui/material";
import { useUpdateProduct } from "../hooks/useUpdateProduct";
import { useGetCategories } from "../hooks/useGetCategories";



const EditProductForm = ({ product }) => {
  const { register, handleSubmit } = useForm({ defaultValues: product });
  const { mutateAsync: updateProduct } = useUpdateProduct();
  const { data: categories = [] } = useGetCategories();

  const onSubmit = async (data: any) => {
    await updateProduct(data);
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{ display: "flex", flexDirection: "column", gap: "1rem" }}
    >
      <TextField label="ID" {...register("id")} disabled />
      <TextField label="Title" {...register("title", { required: true })} />
      <TextField label="Description" {...register("description")} />
      <TextField
        label="Price"
        type="number"
        {...register("price", { required: true })}
      />
      <TextField label="Image URL" {...register("imageUrl")} />
      <Select label="Category" {...register("category.id", { required: true })}>
        {categories.map((cat) => (
          <MenuItem key={cat.id} value={cat.id}>
            {cat.title}
          </MenuItem>
        ))}
      </Select>
      <Button type="submit" variant="contained">
        Update Product
      </Button>
    </Box>
  );
};

export default EditProductForm;
