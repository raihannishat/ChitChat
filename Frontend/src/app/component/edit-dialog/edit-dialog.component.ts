import { Component, Inject, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  ValidatorFn,
  ValidationErrors,
  AsyncValidatorFn,
  AbstractControl,
} from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { User } from '../../models/user.model';
import { RegistrationComponent } from '../registration/registration.component';
import { UsersService } from '../../services/users.service';
import { UserValidator } from '../../shared/user.validator';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Toast, ToastrService } from 'ngx-toastr';
import moment from 'moment';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css'],
})
export class EditDialogComponent implements OnInit {
  user: User = {
    name: '',
    dateOfBirth: new Date(),
    password: '',
    email: '',
  };

  constructor(
    private fb: FormBuilder,
    private userService: UsersService,
    private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public editData: User,
    private dialogRef: MatDialogRef<EditDialogComponent>
  ) {}

  userEditForm = this.fb.group({
    userName: [''],
    email: ['', [Validators.required, Validators.email]],
    password: [''],
    dateOfBirth: ['', [Validators.required, this.AgeChek('dateOfBirth')]], //this.youngerThanValidator(18)]]
  });

  ngOnInit(): void {
    if (this.editData) {
      this.userEditForm.controls['userName'].setValue(this.editData.name);
      this.userEditForm.controls['email'].setValue(this.editData.email);
      this.userEditForm.controls['password'].setValue(this.editData.password);
      this.userEditForm.controls['dateOfBirth'].setValue(
        this.editData.dateOfBirth
      );
      this.userEditForm.controls['userName'].disable();
      this.userEditForm.controls['password'].disable();
    }
  }

  errorMethod() {
    return this.userEditForm?.errors && this.userEditForm?.errors['younger'];
  }

  updateUser() {
    this.user.name = this.userEditForm.controls['userName'].value;
    this.user.password = this.userEditForm.controls['password'].value;
    this.user.email = this.userEditForm.controls['email'].value;
    this.user.dateOfBirth = this.userEditForm.controls['dateOfBirth'].value;

    this.userService
      .updateUser(String(this.editData?.id), this.user)
      .subscribe({
        next: () => {
          this.userEditForm.reset();
          this.dialogRef.close('update');
          this.toastr.success('Successfully', 'Update');
        },
        error: () => {
          alert('Error while updating user profile');
        },
      });
  }

  // deleteUser() {
  //   this.userService.deleteUser((String)(this.editData.id));
  // }
  AgeChek(controlName: string): ValidatorFn {
    return (abstractCotrol: AbstractControl) => {
      if (this.age18Check(abstractCotrol?.value)) {
        return { younger: true };
      } else {
        return null;
      }
    };
  }

  age18Check(birthDay: Date) {
    const bornDate = moment(birthDay).add(18, 'years');
    const now = moment();
    return bornDate > now;
  }
}
