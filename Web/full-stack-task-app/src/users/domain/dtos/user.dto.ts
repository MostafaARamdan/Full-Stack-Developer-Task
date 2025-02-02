export interface UserDTO {
  id?: string;
  username: string;
  password?: string;
  confirmPassword?: string;
  email: string;
  fullName: string;
  roleIds: string[];
  createdBy?: string;
  isDeleted: boolean;
}
