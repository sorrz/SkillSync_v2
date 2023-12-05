import { test, assert } from 'vitest';
import { describe, afterEach } from 'vitest';
import { render, screen, cleanup } from '@testing-library/react';
import StudentItem from '../Components/StudentItem';
import StudentModel from '../Models/StudentModel';

const mockStudent: StudentModel = {
    Id: 1,
    Name: 'John Doe',
    ImageUrl: 'path-to-mock-image',
    TechStack: ['React', 'JavaScript'],
    Presentation: 'Test presentation',
    PhoneNumber: '123-456-7890',
    Graduation: '2023',
    StartLia1: '2022-01-01',
    EndLia1: '2022-06-30',
    StartLia2: '2022-07-01',
    EndLia2: '2022-12-31',
    MailAddress: 'john.doe@drMartins.com',
    PasswordHash: '',
    connectedTo: []
};

describe('CompanyItem Component', () => {
    afterEach(() => {
      cleanup();
    });

test('renders StudentItem component with correct content', async () => {
  render(<StudentItem student={mockStudent} />);

  const studentItem = screen.getByTestId('studentCard');
  assert.isNotNull(studentItem);

  assert.isNotNull(screen.queryByText('John Doe'));
  assert.isNotNull(screen.queryByText('React, JavaScript'));
  assert.isNotNull(screen.queryByText('Test presentation'));
  assert.isNotNull(screen.queryByText('Phone: 123-456-7890 | Graduation: 2023 | LIA 1: 2022-01-01 - 2022-06-30 | LIA 2: 2022-07-01 - 2022-12-31'));
});


});