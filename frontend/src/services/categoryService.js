import axiosClient from '../api/axiosClient';

const categoryService = {
  getAllCategories: async () => {
    const response = await axiosClient.get('/Categories');
    return response.data.categories || response.data || [];
  },

  createCategory: async (categoryData) => {
    return await axiosClient.post('/Categories/create-category', categoryData);
  },

  createBulkCategories: async (categoriesArray) => {
    return await axiosClient.post('/Categories/create-bulk-categories', { categories: categoriesArray });
  }
};

export default categoryService;