import { test, assert, vi, expect } from 'vitest';
import { describe, afterEach } from 'vitest';
import { fireEvent, render, screen, cleanup } from '@testing-library/react';
import { Navbar } from '../Components/Navbar';
import { BrowserRouter as Router } from 'react-router-dom';
import StudentModel from '../Models/StudentModel';


const mockOnLogout = vi.fn();

describe('CompanyItem Component', () => {
    afterEach(() => {
      cleanup();
    });


const mockUser: StudentModel = {
    id: 11,
    name: "Anna Jelbrink",
    mailAddress: "anna@edu.tucsweden.se",
    passwordHash: "",
    techStack: ["c#", "js", "node", "sql"],
    phoneNumber: "+46 666 666 666",
    graduation: "0001-01-01T00:00:00",
    startLia1: "0001-01-01T00:00:00",
    endLia1: "0001-01-01T00:00:00",
    startLia2: "0001-01-01T00:00:00",
    endLia2: "0001-01-01T00:00:00",
    presentation: "I'm a girly girl that likes tech, uWu",
    imageUrl: "https://media.glamour.com/photos/5695cb3b16d0dc3747ee0717/master/w_1600%2Cc_limit/fashionbeauty__fashion-images-fash080221-1.jpg",
    connectedTo: [],
    linkedInProfile: "string"
};

test('renders Navbar component when not logged in', async () => {
  render(
    <Router>
      <Navbar isLoggedIn={false} user={mockUser} onLogout={() => {}} />
    </Router>
  );

  const navbar = screen.queryByRole('navigation');
  assert.isNotNull(navbar);

  assert.isNotNull(screen.queryByText('Home'));
  assert.isNotNull(screen.queryByText('Companies'));
  assert.isNotNull(screen.queryByText('Students'));
  assert.isNotNull(screen.queryByText('About'));

  assert.isNotNull(screen.queryByText('Login'));
});

test('renders Navbar component when logged in', async () => {
  render(
    <Router>
      <Navbar isLoggedIn={true} user={mockUser} onLogout={() => {}} />
    </Router>
  );

  const navbar = screen.queryByRole('navigation');
  assert.isNotNull(navbar);

  assert.isNotNull(screen.queryByText('Home'));
  assert.isNotNull(screen.queryByText('Companies'));
  assert.isNotNull(screen.queryByText('Students'));
  assert.isNotNull(screen.queryByText('About'));

  assert.isNotNull(screen.getByTestId('profile-id'));
  assert.isNotNull(screen.getByTestId('actionButton'));
});

test('calls onLogout function when the logout button is clicked', async () => {
  render(
    <Router>
      <Navbar isLoggedIn={true} user={mockUser} onLogout={mockOnLogout} />
    </Router>
  );

  const button = screen.getByTestId('actionButton');
  fireEvent.click(button)
  expect(mockOnLogout).toHaveBeenCalled();
});

});
