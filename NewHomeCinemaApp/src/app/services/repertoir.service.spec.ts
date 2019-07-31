import { TestBed } from '@angular/core/testing';

import { RepertoirService } from './repertoir.service';

describe('RepertoirService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RepertoirService = TestBed.get(RepertoirService);
    expect(service).toBeTruthy();
  });
});
