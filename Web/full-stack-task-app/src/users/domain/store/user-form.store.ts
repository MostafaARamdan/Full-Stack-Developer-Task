import { Injectable } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { exhaustMap, switchMap, tap } from 'rxjs/operators';
import { UserDTO } from '../dtos/user.dto';
import { UserService } from '../services/user.service';
import { ResponseStatus } from '../../../shared/util-common/domain/constants/response-status.enum';
import { RoleDto } from '../dtos/role.dto';
import { Router } from '@angular/router';
import { NotificationService } from '../../../shared/util-common/domain/notifications/notification.service';

interface UserFormState {
  user: UserDTO | null;
  roles: RoleDto[];
  loading: boolean;
  error: string[] | null;
}

@Injectable()
export class UserFormStore extends ComponentStore<UserFormState> {
  constructor(
    private userService: UserService,
    private _router: Router,
    private notificationService: NotificationService
  ) {
    super({ user: null, roles: [], loading: false, error: null });
  }

  readonly user$ = this.select((state) => state.user);
  readonly loading$ = this.select((state) => state.loading);
  readonly error$ = this.select((state) => state.error);
  readonly roles$ = this.select((state) => state.roles);

  readonly loadUser = this.effect<string | null>((userId$) =>
    userId$.pipe(
      tap(() => this.patchState({ loading: true })),
      switchMap((userId) =>
        this.userService.getUser(userId).pipe(
          tap({
            next: (response) => {
              if (response.status == ResponseStatus.Success) {
                const userDetailsDTO = response.resource.user;
                const roles = response.resource.roles;
                this.patchState({
                  user: {
                    email: userDetailsDTO.email,
                    fullName: userDetailsDTO.fullName,
                    username: userDetailsDTO.username,
                    id: userDetailsDTO.id,
                    roleIds:
                      userDetailsDTO.userRoles?.map((s) => s.roleId) ?? [],
                    createdBy:
                      userDetailsDTO.createdBy ??
                      '85ED7233-602F-47E3-AFCB-B1AA8BE36CF7',
                    isDeleted: userDetailsDTO.isDeleted,
                  } as UserDTO,
                  roles: roles,
                  loading: false,
                });
              } else
                this.patchState({ error: response.messages, loading: false });
            },
            error: (error) =>
              this.patchState({ error: error.message, loading: false }),
          })
        )
      )
    )
  );

  readonly addUser = this.effect<UserDTO>((user$) =>
    user$.pipe(
      tap(() => this.patchState({ loading: true })),
      exhaustMap((user) => {
        return this.userService.addUser(user).pipe(
          tap({
            next: (res) => {
              this.patchState({ loading: false });
              if (res.status == ResponseStatus.Success)
                this._router.navigate(['/users']);
              else
                res.messages.forEach((message) => {
                  this.notificationService.showError(message);
                });
            },
            error: (error) =>
              this.patchState({ error: error.message, loading: false }),
          })
        );
      })
    )
  );

  readonly updateUser = this.effect<UserDTO>((user$) =>
    user$.pipe(
      tap(() => this.patchState({ loading: true })),
      exhaustMap((user) =>
        this.userService.updateUser(user).pipe(
          tap({
            next: (res) => {
              this.patchState({ loading: false });
              if (res.status == ResponseStatus.Success)
                this._router.navigate(['/users']);
              else
                res.messages.forEach((message) => {
                  this.notificationService.showError(message);
                });
            },
            error: (error) =>
              this.patchState({ error: error.message, loading: false }),
          })
        )
      )
    )
  );
}
