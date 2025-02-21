import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { NewOrderComponent } from '../new-order/new-order.component';
import { OrdersViewComponent } from '../orders-view/orders-view.component';
import { Customer } from './customer-list.component';
import { CustomerListService } from './customer-list.service'; // Asegúrate de tener un servicio para obtener los datos de clientes



@Component({
    selector: 'app-customer-list',
    standalone: true,
    imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatDialogModule, HttpClientModule],
    templateUrl: './customer-list.component.html',
    styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
    displayedColumns: string[] = ['name', 'lastOrderDate', 'nextOrderDate', 'actions'];
    dataSource: MatTableDataSource<Customer> | undefined;

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;

    constructor(private customerListService: CustomerListService, public dialog: MatDialog) { }

    ngOnInit() {
        this.loadCustomers();
    }

    loadCustomers() {
        this.customerListService.getCustomers().subscribe((data: any) => {
            this.dataSource = new MatTableDataSource(data);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        });
    }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }

    viewOrders(customer: Customer) {
        // Abre un modal con la vista de órdenes del cliente
        const dialogRef = this.dialog.open(OrdersViewComponent, {
            width: '600px',
            data: { customer }
        });
    }

    newOrder(customer: Customer) {
        // Abre un modal con el formulario para crear una nueva orden
        const dialogRef = this.dialog.open(NewOrderComponent, {
            width: '600px',
            data: { customer }
        });
    }
}
