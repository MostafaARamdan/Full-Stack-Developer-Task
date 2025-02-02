export interface RoleDto {
  id: string;
  name: string;
}

export interface UserRoleDto {
  roleId: string;
  roleName: string;
}

export interface UserDto {
  id: string;
  username: string;
  fullName: string;
  email: string;
  isDeleted: boolean;
  userRoles: UserRoleDto[];
}

export interface GetUsersResponse {
  users: UserDto[];
  roles: RoleDto[];
  pagesCount: number;
  currentPage: number;
}
