import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import ReactDOM from 'react-dom';
import Translator, { fetchResourceFromApi } from './Translator';
import Enzyme, { shallow } from "enzyme";
import Adapter from 'enzyme-adapter-react-16'

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

const spyFetchResource = jest.spyOn(Translator.prototype, 'fetchResource');

test('button click should call fetch', () => {
    Enzyme.configure({ adapter: new Adapter() });
    const component = shallow(<Translator />);
    component.find("input.btn-primary").simulate("click");
    expect(spyFetchResource).toHaveBeenCalled();
});