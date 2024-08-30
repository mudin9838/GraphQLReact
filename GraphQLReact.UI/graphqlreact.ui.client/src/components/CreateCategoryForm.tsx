// src/components/CreateCategoryForm.tsx
import { useForm } from "react-hook-form";
import { Box, Button, TextField } from "@mui/material";
import { useCreateCategory } from "../hooks/useCreateCategory";
import { CategoryDtoInput } from "../types/types"; // Adjust import path as necessary

type CreateCategoryFormProps = {
  existingCategory?: CategoryDtoInput; // Pass existing category for editing
};

const CreateCategoryForm = ({ existingCategory }: CreateCategoryFormProps) => {
  const { register, handleSubmit, reset } = useForm<CategoryDtoInput>({
    defaultValues: existingCategory || { id: 0, title: "" }, // Default values for the form
  });

  const { mutateAsync: createCategory, isLoading, error } = useCreateCategory();

  const onSubmit = async (data: CategoryDtoInput) => {
    try {
      if (existingCategory) {
        // Handle edit logic if needed (you might need to implement this in your hook or backend)
        console.log("Editing existing category:", data);
        // You may need a different hook or mutation for updates if necessary
      } else {
        await createCategory(data); // Create new category
      }
      reset(); // Reset form after submission
    } catch (err) {
      console.error("Failed to save category:", err);
    }
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{ display: "flex", flexDirection: "column", gap: "1rem" }}
    >
      <TextField
        label="ID"
        type="number"
        {...register("id", { required: true })}
        error={!!error}
        helperText={error ? "Failed to save category" : ""}
        disabled={!!existingCategory} // Disable ID input if editing
      />
      <TextField
        label="Title"
        {...register("title", { required: true })}
        error={!!error}
        helperText={error ? "Failed to save category" : ""}
      />
      <Button type="submit" variant="contained" disabled={isLoading}>
        {existingCategory
          ? isLoading
            ? "Saving..."
            : "Save Changes"
          : isLoading
          ? "Creating..."
          : "Create Category"}
      </Button>
    </Box>
  );
};

export default CreateCategoryForm;
