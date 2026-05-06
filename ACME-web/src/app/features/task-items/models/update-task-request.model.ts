export interface UpdateTaskRequest {
  title: string;
  description?: string;
  dueDate?: string;
  status: string;
  taskCategoryId?: number | null;
}