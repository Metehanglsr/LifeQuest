import axiosClient from '../api/axiosClient';

const authService = {
  login: async (credentials) => {
    const response = await axiosClient.post('/auth/login', credentials);
    return response.data;
  },
  register: async (data) => {
    const response = await axiosClient.post('/auth/register', data);
    return response.data;
  }
};

export default authService;