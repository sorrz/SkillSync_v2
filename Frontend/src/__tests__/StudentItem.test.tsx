import { describe, it, afterEach, assert } from 'vitest';
import { render, screen, cleanup } from '@testing-library/react';
import StudentItem from '../Components/StudentItem';
import StudentModel from '../Models/StudentModel';

const mockStudent: StudentModel = {
  id: 1,
  name: 'John Doe',
  imageUrl: 'path-to-mock-image',
  techStack: ['React', 'JavaScript'],
  presentation: 'Test presentation',
  phoneNumber: '123-456-7890',
  graduation: '2023',
  startLia1: '2022-01-01',
  endLia1: '2022-06-30',
  startLia2: '2022-07-01',
  endLia2: '2022-12-31',
  mailAddress: 'john.doe@drMartins.com',
  passwordHash: '',
  connectedTo: [],
  linkedInProfile: '',
};

describe('CompanyItem Component', () => {
  afterEach(() => {
    cleanup();
  });

  it('renders StudentItem component with correct content', () => {
    render(<StudentItem student={mockStudent} />);

    const studentItem = screen.getByTestId('studentCard');
    assert.isNotNull(studentItem);

    const namePlate = screen.getByTestId('name-holder');
    assert.isNotNull(namePlate);
    
    
  });
});
