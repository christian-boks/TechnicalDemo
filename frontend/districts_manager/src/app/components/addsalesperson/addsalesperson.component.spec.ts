import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddsalespersonComponent } from './addsalesperson.component';

describe('AddsalespersonComponent', () => {
  let component: AddsalespersonComponent;
  let fixture: ComponentFixture<AddsalespersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddsalespersonComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddsalespersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
