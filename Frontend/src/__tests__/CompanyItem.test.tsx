import { describe, expect, afterEach, test } from 'vitest';
import { cleanup, render, screen } from '@testing-library/react';
import CompanyItem from '../Components/CompanyItem';
import { CompanyModel } from '../Models/CompanyModel';




  const mockCompany: CompanyModel = {
    id: 1,
    name: 'Test Company',
    imgURL: 'https://example.com/image.jpg',
    contactName: 'John Doe',
    contactMail: 'john@example.com',
    contactPhone: '123-456-7890',
    TechStack: ['React', 'TypeScript'],
    presentation: 'Test presentation',
    mentorship: true,
    spots: 5,
    hasExJob: false,
  };

  describe('CompanyItem Component', () => {
    afterEach(() => {
      cleanup();
    });

  test('renders CompanyItem with correct content', async () => {
  
    render(<CompanyItem company={mockCompany} />);
    
    expect(screen.getByTestId('card-id')).not.toBeNull();
    
  });
});
