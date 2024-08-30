import { useForm } from "react-hook-form";
import { Box, Button, TextField, Select, MenuItem } from "@mui/material";
import { useGetCategories } from "../hooks/useGetCategories";
import { useCreateProduct } from "../hooks/useCreateProduct";


const CreateProductForm = () => {
  const { register, handleSubmit } = useForm();
  const { mutateAsync: createProduct } = useCreateProduct();
  const { data: categories = [] } = useGetCategories();

  const onSubmit = async (data: any) => {
    await createProduct(data);
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{ display: "flex", flexDirection: "column", gap: "1rem" }}
    >
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
        Create Product
      </Button>
    </Box>
  );
};

export default CreateProductForm;
