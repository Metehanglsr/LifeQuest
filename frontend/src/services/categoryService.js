import axiosClient from "../api/axiosClient";
const categoryService = {
  getAll: () => axiosClient.get("/categories"),
  create: (data) => axiosClient.post("/categories", data),
  createBulk: (data) => axiosClient.post("/categories/bulk", data),
};
export default categoryService;
