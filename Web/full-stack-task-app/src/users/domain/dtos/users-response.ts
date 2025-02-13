import { RoleDto } from './role.dto';
import { UserDetailsDTO } from './user-details.dto';

export interface GetUsersResponse {
  users: UserDetailsDTO[];
  roles: RoleDto[];
  totalCount: number;
  currentPage: number;
}
