import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { ModalRemoveComponent } from '../modal-remove/modal-remove.component';
import { ModalCreateComponent } from '../modal-create/modal-create.component';
import { Contacto } from '../models/contacto.interface';
import { ECrudType } from '../enums/crud-type.enum';

@Component({
  selector: 'app-contactos',
  templateUrl: './contactos.component.html'
})
export class ContatosComponent {

  public contactos: Contacto[];
  buscar: string = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private modalService: NgbModal) {
    this.loadContactos();    
  }

  loadContactos() {

    this.http.get<Contacto[]>(this.baseUrl + 'api/contactos').subscribe(result => {
      this.contactos = result;
    }, error => console.error(error));

  }

  addContacto() {

    let contacto: Contacto = { id: 0, nombre: '', direccion: '', telefono: '', curp: '', fechaRegistro: new Date() };

    const modalRef = this.modalService.open(ModalCreateComponent);
    modalRef.componentInstance.contacto = contacto;
    modalRef.componentInstance.crudType = ECrudType.SAVE;
    modalRef.result.then(((result: Contacto) => {

      //result.fechaRegistro = new Date();

      this.http.post(this.baseUrl + 'api/contactos', result).subscribe(result => {
        console.log(result);
        this.loadContactos();
      }, error => console.log(error));
      

    }).bind(this))
  }
  
  edit(contacto: Contacto) {
    const modalRef = this.modalService.open(ModalCreateComponent);
    modalRef.componentInstance.contacto = contacto;
    modalRef.componentInstance.crudType = ECrudType.EDIT;
    modalRef.result.then(((result: Contacto) => {

      //result.fechaRegistro = new Date();

      this.http.put(this.baseUrl + 'api/contactos', result).subscribe(result => {
        console.log(result);
        this.loadContactos();
      }, error => console.log(error));


    }).bind(this))
  }

  remove(contacto: Contacto) {
    const modalRef = this.modalService.open(ModalRemoveComponent);
    modalRef.componentInstance.contacto = contacto;
    modalRef.result.then(((result) => {
      console.log(result);

      if (result === 'Ok') {

        this.http.delete(this.baseUrl + 'api/contactos/' + contacto.id).subscribe(result => {
          console.log(result);
          this.loadContactos();
        }, error => console.log(error));
      }
    }).bind(this))
  }
}
