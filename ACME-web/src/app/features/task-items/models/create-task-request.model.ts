export interface CreateTaskRequest {
  title: string;
  description?: string;
  dueDate?: string;
  taskCategoryId?: number | null;
}