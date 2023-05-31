import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OpenWaterTestDto } from 'src/types/dto/open-water-test/OpenWaterTestDto';

@Component({
  selector: 'open-water-test-content-dialog',
  templateUrl: './open-water-test-content-dialog.component.html',
  styleUrls: ['./open-water-test-content-dialog.component.scss']
})
export class OpenWaterTestContentDialogComponent implements OnInit {

  openWaterTest: OpenWaterTestDto

  constructor(@Inject(MAT_DIALOG_DATA) data) { 
    this.openWaterTest = data
  }

  ngOnInit(): void {

  }

}