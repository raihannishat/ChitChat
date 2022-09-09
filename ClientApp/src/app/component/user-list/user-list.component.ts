import { Component, OnInit } from '@angular/core';
import { AfterViewInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { UsersService } from 'src/app/services/users.service';
import { User } from 'src/app/models/user.model';
import { EditDialogComponent } from '../edit-dialog/edit-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Toast, ToastrService } from 'ngx-toastr';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'email', 'dateOfBirth', 'action'];
  dataSource!: MatTableDataSource<User>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  isCompleted = false;
  constructor(
    private userService: UsersService,
    private dialog: MatDialog,
    private toastr: ToastrService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    // this.authService.decodeToken();
    this.getAllUsers();
  }

  getAllUsers() {
    this.isCompleted = true;
    this.userService.getAllUsers().subscribe({
      next: (res) => {
        // console.log(res);
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isCompleted = false;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  editUser(row: User) {
    this.dialog
      .open(EditDialogComponent, {
        width: '30%',
        data: row,
      })
      .afterClosed()
      .subscribe((val) => {
        if (val === 'update') {
          this.getAllUsers();
        }
      });
  }

  deleteUser(id: string) {
    this.dialog
      .open(DeleteDialogComponent, {
        width: '30%',
        data: id,
      })
      .afterClosed()
      .subscribe((val) => {
        if (val === 'delete') {
          this.getAllUsers();
        }
      });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
