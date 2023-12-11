import { test, assert } from 'vitest';
import { describe, afterEach } from 'vitest';
import { render, screen, cleanup } from '@testing-library/react';
import Register from '../Pages/Register';
import StudentModel from '../Models/StudentModel';


describe('Register', () => {
    afterEach(() => {
      cleanup();
    });


test('renders Register component correctly', async () => {
    render(<Register handleRegistration={handleRegistrationMock} />);

    const registerContainer = screen.getByTestId('register-container');
    assert.isNotNull(registerContainer);
    assert.equal(registerContainer.classList.contains('container'), true);
});


test('Register component contains correct content', async () => {
    render(<Register handleRegistration={handleRegistrationMock} />);

    const registerContainer = screen.getByTestId('register-container');
    assert.isNotNull(registerContainer);
    
    assert.equal(registerContainer.classList.contains('container'), true);
    assert.equal(registerContainer.innerHTML.includes('Register'), true);
});


});

function handleRegistrationMock(_student: StudentModel): void {
    throw new Error('Function not implemented.');
}
