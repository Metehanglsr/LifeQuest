import axiosClient from '../api/axiosClient';

const taskService = {
  
  getAll: () => axiosClient.get('/tasks/getall'),
  
  complete: (taskId) => axiosClient.post('/tasks/complete', { taskId: taskId }),


  create: (data) => axiosClient.post('/tasks/create-task', data),
  
  createBulk: (data) => axiosClient.post('/tasks/create-bulk-tasks', data),
};

export default taskService;