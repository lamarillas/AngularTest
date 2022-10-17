import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Contacto } from '../models/contacto.interface';

@Component({
  selector: 'app-modal-remove',
  templateUrl: './modal-remove.component.html',
  styleUrls: ['./modal-remove.component.css']
})
export class ModalRemoveComponent implements OnInit {
  @Input() contacto: Contacto;
  constructor(public modal: NgbActiveModal) { }

  ngOnInit() {

  }

}
