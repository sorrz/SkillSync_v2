import { expect, test, vi } from 'vitest';
import { describe, afterEach } from 'vitest';
import { render, screen, cleanup, fireEvent } from '@testing-library/react';
import ConnectToItem from '../Components/ConnectToItem';

const mockOnLike = vi.fn();

describe('CompanyItem Component', () => {
    afterEach(() => {
      cleanup();
    });

test('renders ConnectToItem with correct content when not liked', async () => {
  render(<ConnectToItem onLike={mockOnLike} isLiked={false} />);
  
  const button = screen.queryByRole('button');
  expect(button).not.toBeNull();

  const element = screen.getByTestId('connectButton');
  expect(element.innerText).toContain('Click');
    
});

test('renders ConnectToItem with correct content when liked', async () => {
    render(<ConnectToItem onLike={mockOnLike} isLiked={true} />);
    
    const button = screen.queryByRole('button');
    expect(button).not.toBeNull();
  
    const element = screen.getByTestId('connectButton');
    expect(element.innerText).toContain('shown')
      
  });

test('calls onLike function when the button is clicked', async () => {
  render(<ConnectToItem onLike={mockOnLike} isLiked={false} />);
  
    const button = screen.getByTestId('connectButton');
     fireEvent.click(button);
    expect(mockOnLike).toHaveBeenCalled();
});


});
