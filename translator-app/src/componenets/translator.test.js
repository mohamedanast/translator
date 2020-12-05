import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import ReactDOM from 'react-dom';
import Translator, { fetchResourceFromApi } from './Translator';

it('renders input controls', () => {
    render(<Translator />);
    expect(screen.getByText('localized resource', { exact: false })).toBeInTheDocument();
    expect(screen.getByText('Choose language')).toBeInTheDocument();
    expect(screen.getByRole('textbox', { name: 'resource' })).toBeInTheDocument();
    expect(screen.getByRole('textbox', { name: 'resource group' })).toBeInTheDocument();
    expect(screen.getByText('Get resource')).toBeInTheDocument();
});

it('renders without crashing', () => {
    const div = document.createElement('div');
    ReactDOM.render(<Translator />, div);
});

it('should retreive the results correctly', () => {
    return fetchResourceFromApi('en', 'Common', 'OKButtonText').then(data => {
        expect(data).toBe('OK');
    });
});

test('should return error for non-existing resource', async () => {
    await expect(fetchResourceFromApi('en', 'Common', 'OK-ButtonText')).rejects.toThrow('[object Promise]');
});
