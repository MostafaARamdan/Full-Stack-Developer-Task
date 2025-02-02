import { UserRoleDto } from './user-role.dto';

export interface UserDetailsDTO {
  id: string;
  username: string;
  fullName: string;
  email: string;
  isDeleted: boolean;
  userRoles: UserRoleDto[];
  created: Date;
  createdBy: string | null;
  modified?: Date | null;
  modifiedBy?: string | null;
}
