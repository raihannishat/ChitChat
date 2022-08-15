import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  ValidatorFn,
  ValidationErrors,
  AsyncValidatorFn,
} from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FormArray } from '@angular/forms';
import { AbstractControl } from '@angular/forms';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { delay, map, Observable, of } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { UsersService } from 'src/app/services/users.service';
import { UserValidator } from 'src/app/shared/user.validator';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})
export class RegistrationComponent implements OnInit {
  profileForm = new FormGroup({});
  users: User[] = [];
  user: User = {
    name: '',
    dateOfBirth: new Date(),
    password: '',
    email: '',
  };
  constructor(
    private fb: FormBuilder,
    private userService: UsersService,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  profile() {
    this.profileForm = this.fb.group({
      userName: [
        '',
        [Validators.minLength(3), Validators.required],
        [UserValidator.nameValidator(this.userService)],
      ],
      email: [
        '',
        [Validators.required, Validators.email],
        [UserValidator.emailValidator(this.userService)],
      ],
      password: [
        '',
        [
          Validators.required,
          Validators.pattern(
            '^(?=.*[A-Za-z])(?=.*?[0-9])(?=.*?[!@#$&*~]).{6,}$'
          ),
        ],
      ],
      confirmPassword: ['', [Validators.required, this.ValidatingPassword()]],
      dateOfBirth: ['', [Validators.required, this.AgeChek()]],
    });
  }

  getAllUsers() {
    this.userService.getAllUsers().subscribe((response) => {
      this.users = response;
      console.log(response);
    });
  }

  async onSubmit() {
    if (this.profileForm.invalid) return;
    this.storeFormData();
    const user = JSON.parse(JSON.stringify(this.user));
    delete user.id;
    this.authService.signUp(user).subscribe(
      (response) => {
        this.profileForm = this.fb.group({
          userName: [''],
          email: [''],
          password: [''],
          confirmPassword: [''],
          dateOfBirth: [''],
        });
        this.toastr.success('Successful !', 'Registration');
        // this.refresh();
      },
      (err) => {
        this.toastr.error('Please check the form again', 'Invalid request');
      }
    );
  }
  refresh(): void {
    window.location.reload();
  }
  ngOnInit(): void {
    this.profile();
    // this.getAllUsers();
    // this.profileForm.controls['confirmPassword'].valueChanges.subscribe(
    //   (value) => console.log(this.profileForm)
    // );
  }

  storeFormData() {
    this.user.name = this.profileForm.value.userName;
    this.user.dateOfBirth = this.profileForm.value.dateOfBirth;
    this.user.password = this.profileForm.value.password;
    this.user.email = this.profileForm.value.email;
  }

  ValidatingPassword(): ValidatorFn {
    return (abstractControl: AbstractControl): ValidationErrors | null => {
      let confirmPassword = abstractControl.value;
      if (this.profileForm.get('password')?.value === confirmPassword) {
        return null;
      } else {
        return { misMatch: true };
      }
    };
  }
  /*
  MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors?.['mustMatch']) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }
*/

  AgeChek(): ValidatorFn {
    return (abstractControl: AbstractControl) => {
      if (this.age18Check(abstractControl?.value)) {
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

  get userName() {
    return this.profileForm.get('userName');
  }

  get email() {
    return this.profileForm.get('email');
  }

  get password() {
    return this.profileForm.get('password');
  }

  get confirmPassword() {
    return this.profileForm.get('confirmPassword');
  }

  get dateOfBirth() {
    return this.profileForm.get('dateOfBirth');
  }
}
