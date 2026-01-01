import axiosClient from '../api/axiosClient';
const badgeService = {
  create: (data) => axiosClient.post('/badges', data),
  createBulk: (data) => axiosClient.post('/badges/bulk', data),
};
export default badgeService;