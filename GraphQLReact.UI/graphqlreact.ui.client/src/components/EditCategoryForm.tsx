import { useForm } from "react-hook-form";
import { Box, Button, TextField } from "@mui/material";
import { useUpdateCategory } from "../hooks/useUpdateCategory";
import { CategoryDtoInput } from "../types/types";

type EditCategoryFormProps = {
  category: CategoryDtoInput; // Expect CategoryDtoInput for editing
};

const EditCategoryForm = ({ category }: EditCategoryFormProps) => {
  const { register, handleSubmit } = useForm<CategoryDtoInput>({
    defaultValues: category,
  });
  const { mutateAsync: updateCategory } = useUpdateCategory();

  const onSubmit = async (data: CategoryDtoInput) => {
    console.log("Submitting category:", data); // Debug output to verify data

    const updatedValues: CategoryDtoInput = {
      id: parseInt(data.id as unknown as string, 10), // Convert to integer
      title: data.title,
    };

    try {
      await updateCategory(updatedValues);
      console.log("Category updated successfully"); // Success log
    } catch (error) {
      console.error("Failed to update category:", error); // Error log
    }
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)} // Ensure this triggers onSubmit
      sx={{ display: "flex", flexDirection: "column", gap: "1rem" }}
    >
      <TextField
        label="ID"
        type="number"
        {...register("id", { required: true })}
      />
      <TextField label="Title" {...register("title", { required: true })} />
      <Button type="submit" variant="contained">
        Update Category
      </Button>
    </Box>
  );
};

export default EditCategoryForm;
