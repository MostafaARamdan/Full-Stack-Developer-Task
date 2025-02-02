import { RoleDto } from './role.dto';
import { UserDetailsDTO } from './user-details.dto';

export interface GetUserByIdResponse {
  user: UserDetailsDTO;
  roles: RoleDto[];
}
