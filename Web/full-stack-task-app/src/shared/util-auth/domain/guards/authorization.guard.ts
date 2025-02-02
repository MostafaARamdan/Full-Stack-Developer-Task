import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { NgxPermissionsService } from 'ngx-permissions';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationGuard implements CanActivate {
  constructor(
    private permissionsService: NgxPermissionsService,
    private router: Router
  ) {}

  async canActivate(next: ActivatedRouteSnapshot): Promise<boolean> {
    const requiredPermissions = next.data['permissions'] as string[];
    const hasPermissions = await this.permissionsService.hasPermission(
      requiredPermissions
    );
    if (hasPermissions) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
