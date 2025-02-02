export interface GetUsersQuery {
  username?: string | null;
  fullName?: string | null;
  email?: string | null;
  isDeleted?: boolean | null;
  roleId?: string | null;
  multipleSortWithDir?: string | null;
  pageNumber: number;
  pageSize: number;
}
