import { Component, Input, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { UserDTO } from '../../domain/dtos/user.dto';
import { UserFormStore } from '../../domain/store/user-form.store';
import { CommonModule } from '@angular/common';
import { Observable, take } from 'rxjs';
import { RoleDto } from '../../domain/dtos/role.dto';

@Component({
  selector: 'app-persist-user',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterModule],
  providers: [UserFormStore],
  templateUrl: './persist-user.component.html',
  styleUrl: './persist-user.component.scss',
})
export class PersistUserComponent implements OnInit {
  editMode!: boolean;
  userForm!: FormGroup;
  roles$!: Observable<RoleDto[]>; // Should be populated from service

  constructor(
    private fb: FormBuilder,
    private store: UserFormStore,
    private route: ActivatedRoute
  ) {
    this.roles$ = this.store.roles$;
    this.userForm = this.fb.group(
      {
        id: [null],
        username: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(100),
          ],
        ],
        password: [''],
        confirmPassword: [''],
        email: ['', [Validators.required, Validators.email]],
        fullName: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(255),
          ],
        ],
        roleIds: [[], [Validators.required]],
        isDeleted: [false],
        createdBy: [''],
      },
      { validator: this.passwordMatchValidator(this.editMode) }
    );
  }

  ngOnInit() {
    const userId = history.state.userid as string;
    this.editMode = userId != undefined;
    this.store.loadUser(userId ?? '');
    this.store.user$.subscribe((user) => {
      if (user) {
        this.userForm.patchValue({
          ...user,
          password: '',
          confirmPassword: '',
        });
      }
    });
  }

  passwordMatchValidator(editMode: boolean) {
    return (form: FormGroup) => {
      const password = form.get('password')?.value;
      const confirmPassword = form.get('confirmPassword')?.value;

      if (!editMode && !password) {
        form.get('password')?.setErrors({ required: true });
        return { required: true };
      }

      if (password || confirmPassword) {
        return password === confirmPassword ? null : { mismatch: true };
      }
      return null;
    };
  }

  onSubmit() {
    if (this.userForm.valid) {
      const user: UserDTO = this.userForm.value;
      if (this.editMode) {
        this.store.updateUser(user);
      } else {
        this.store.addUser(user);
      }
    }
  }
}
