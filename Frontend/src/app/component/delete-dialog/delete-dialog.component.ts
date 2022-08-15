import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Toast, ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user.model';
import { UsersService } from 'src/app/services/users.service';
@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.css'],
})
export class DeleteDialogComponent implements OnInit {
  constructor(
    private userService: UsersService,
    private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public deleteData: User,
    private dialogRef: MatDialogRef<DeleteDialogComponent>
  ) {}

  deleteUser() {
    this.userService.deleteUser(String(this.deleteData?.id)).subscribe({
      next: () => {
        this.dialogRef.close('delete');
        this.toastr.success('Successfully', 'Delete');
      },
      error: () => {
        alert('Error while deleting user profile');
      },
    });
  }

  ngOnInit(): void {}
}
