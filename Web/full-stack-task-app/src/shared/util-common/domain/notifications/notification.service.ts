import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export abstract class NotificationService {
  constructor(private toastr: ToastrService) {}
  clear() {
    this.toastr.clear();
  }
  showError(message?: string, title?: string): void {
    message = message || 'There was an error';
    title = title || '';
    this.toastr.error(message, title);
  }

  showSuccess(message?: string, title?: string) {
    message = message || '';
    title = title || '';

    this.toastr.success(message, title);
  }
}
