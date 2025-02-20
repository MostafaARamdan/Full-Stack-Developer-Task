import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

import { KENDO_DIALOGS, KENDO_DIALOG } from '@progress/kendo-angular-dialog';
import {
  DataStateChangeEvent,
  FilterableSettings,
  KENDO_GRID_FILTER_ROW,
  GridDataResult,
  KENDO_GRID,
  PageChangeEvent,
} from '@progress/kendo-angular-grid';
import { UserListStore } from '../../domain/store/user-list.store';
import {
  CompositeFilterDescriptor,
  SortDescriptor,
  State,
} from '@progress/kendo-data-query';

import { Observable } from 'rxjs';
import { KENDO_BUTTON } from '@progress/kendo-angular-buttons';
import { UserDetailsDTO } from '../../domain/dtos/user-details.dto';
import { PermissionsService } from '../../../shared/util-auth/domain/services/permissions.service';
@Component({
  selector: 'app-users-list',
  standalone: true,
  providers: [UserListStore],
  imports: [
    CommonModule,
    KENDO_GRID,
    FormsModule,
    RouterModule,
    KENDO_BUTTON,
    KENDO_DIALOGS,
    KENDO_DIALOG,
  ],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.scss',
})
export class UsersListComponent implements OnInit {
  vm$!: Observable<any>;
  public buttonCount = 5;
  public canEdit = false;
  public canDelete = false;
  public canAdd = false;
  public info = true;
  public pageSizes = [5, 10, 20];
  public filterMode: FilterableSettings = 'row';
  state: State = { skip: 0, take: 5, sort: [], filter: undefined };
  deleteConfirmation = false;
  selectedUser: any;
  constructor(
    private userListStore: UserListStore,
    private router: Router,
    private permissionsService: PermissionsService
  ) {
    this.vm$ = this.userListStore.vm$;
    this.canAdd =
      this.canEdit =
      this.canDelete =
        this.permissionsService.hasPermission('Admin');
  }

  ngOnInit(): void {
    this.loadInitialData();
  }

  private loadInitialData() {
    this.userListStore.setPagination({
      pageSize: 5,
      currentPage: 1,
    });
    this.userListStore.loadUsers();
  }

  dataStateChange(state: DataStateChangeEvent): void {
    const pageSize = state.take || 5;
    const currentPage = (state.skip || 0) / pageSize + 1;
    this.state = state;
    this.userListStore.setSorting(state.sort ?? []);
    this.userListStore.setPagination({ pageSize, currentPage });
    this.userListStore.setFilter(state.filter);
    this.userListStore.loadUsers();
  }

  pageChange(event: PageChangeEvent): void {
    const pageSize = event.take;
    const currentPage = event.skip / pageSize + 1;

    this.userListStore.setPagination({ pageSize, currentPage });
    this.userListStore.loadUsers();
  }
  editUser(user: UserDetailsDTO) {
    this.router.navigate(['/users/edit'], {
      state: { userid: user.id },
    });
  }

  confirmDelete(user: UserDetailsDTO) {
    this.selectedUser = user;
    this.deleteConfirmation = true;
  }

  deleteUser() {
    this.deleteConfirmation = false;
    this.userListStore.deleteUser(this.selectedUser.id);
  }
}
