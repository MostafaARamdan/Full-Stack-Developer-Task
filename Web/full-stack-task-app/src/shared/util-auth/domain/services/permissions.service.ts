import { Injectable } from '@angular/core';
import { NgxPermissionsService } from 'ngx-permissions';
import { jwtDecode } from 'jwt-decode';

@Injectable({ providedIn: 'root' })
export class PermissionsService {
  constructor(private permissionsService: NgxPermissionsService) {}

  private extractRolesFromToken(token: string): string[] {
    const decodedToken = jwtDecode(token) as any;
    const roles = decodedToken?.role;
    return Array.isArray(roles) ? roles : roles ? [roles] : [];
  }
  loadPermissions(token: string | undefined) {
    this.permissionsService.flushPermissions();
    if (!token) return;
    const roles = this.extractRolesFromToken(token);
    if (roles.length > 0) {
      this.permissionsService.loadPermissions(roles);
    }
  }
  hasPermission(permission: string) {
    return this.permissionsService.getPermission(permission) !== undefined;
  }
  clearPermissions() {
    this.permissionsService.flushPermissions();
  }
}
