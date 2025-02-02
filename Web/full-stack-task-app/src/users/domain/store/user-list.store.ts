// user-list.store.ts
import { ComponentStore } from '@ngrx/component-store';
import { Injectable } from '@angular/core';
import { tap, switchMap, withLatestFrom, debounceTime } from 'rxjs/operators';
import {
  CompositeFilterDescriptor,
  FilterDescriptor,
  SortDescriptor,
} from '@progress/kendo-data-query';
import { UserService } from '../services/user.service';
import { GetUsersResponse, RoleDto, UserDto } from '../dtos/users-response';
import { GetUsersQuery } from '../dtos/users.query';
import { ResponseStatus } from '../../../shared/domain/constants/response-status.enum';

export interface UserListState {
  users: UserDto[];
  roles: RoleDto[];
  loading: boolean;
  total: number;
  pageSize: number;
  currentPage: number;
  filter: any; // Can be further typed based on your filter requirements
  sort: SortDescriptor[];
}

interface PaginationParams {
  pageSize: number;
  currentPage: number;
}

const initialState = {
  users: [],
  roles: [],
  loading: false,
  total: 0,
  pageSize: 5,
  currentPage: 1,
  filter: {},
  sort: [],
} as UserListState;

@Injectable()
export class UserListStore extends ComponentStore<UserListState> {
  readonly vm$ = this.select((state) => ({
    users: state.users,
    roles: state.roles,
    loading: state.loading,
    total: state.total,
    pageSize: state.pageSize,
    currentPage: state.currentPage,
  }));

  constructor(private userService: UserService) {
    super(initialState);
  }
  readonly loadUsers = this.effect<void>((trigger$) =>
    trigger$.pipe(
      withLatestFrom(
        this.select((state) => ({
          filter: state.filter,
          sort: state.sort,
          pageNumber: state.currentPage,
          pageSize: state.pageSize,
        }))
      ),
      tap(() => this.patchState({ loading: true })),
      switchMap(([, { filter, sort, pageNumber, pageSize }]) => {
        const query: GetUsersQuery = {
          ...this.mapKendoFilter(filter),
          multipleSortWithDir: this.mapSortToQuery(sort),
          pageNumber: pageNumber,
          pageSize: pageSize,
        };
        return this.userService.getUsers(query);
      }),
      tap((response) => {
        if (response.status == ResponseStatus.Success)
          this.updateState(response.resource);
      }),
      tap(() => this.patchState({ loading: false }))
    )
  );

  readonly updateState = this.updater((state, response: GetUsersResponse) => ({
    ...state,
    users: response.users,
    roles: response.roles,
    total: response.pagesCount,
    currentPage: response.currentPage,
  }));

  readonly setPagination = this.updater((state, params: PaginationParams) => ({
    ...state,
    pageSize: params.pageSize,
    currentPage: params.currentPage,
  }));

  readonly setSorting = this.updater((state, sort: SortDescriptor[]) => ({
    ...state,
    sort,
  }));

  readonly setFilter = this.updater((state, filter: any) => ({
    ...state,
    filter,
  }));

  private mapKendoFilter(
    filter: CompositeFilterDescriptor | undefined
  ): Partial<GetUsersQuery> {
    const filters = filter?.filters as FilterDescriptor[];
    const isDeleted = filters?.find((s) => s.field == 'isDeleted')?.value;
    return {
      username: filters?.find((s) => s.field == 'username')?.value ?? '',
      fullName: filters?.find((s) => s.field == 'fullName')?.value ?? '',
      email: filters?.find((s) => s.field == 'email')?.value ?? '',
      isDeleted:
        isDeleted != null && isDeleted != undefined ? !isDeleted : null,
      roleId: filters?.find((s) => s.field == 'roleId')?.value ?? null,
    };
  }

  private mapSortToQuery(sort: SortDescriptor[]): string {
    return sort
      .map((s) => `${s.dir?.toLowerCase() == 'desc' ? '-' : ''}${s.field}`)
      .join(',');
  }
}
