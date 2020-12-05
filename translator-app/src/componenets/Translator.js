import React from "react";
import LangSelector from './langSelector';
import { Actions, Languages, TranslatorApi } from './constants';
import { format } from 'react-string-format';

class Translator extends React.Component {
    constructor(props) {
        super(props);
        this.state = { language: null, resGroup: '', resource: '', result: '', resultSuccess: false, resultFailure: false };
        this.onUpdate = this.onUpdate.bind(this);
    }

    onUpdate(action, value) {
        switch (action) {
            case Actions.CHANGE_LANG:
                this.setState(prevState => {
                    return { language: value };
                });
                break;
            case Actions.CHANGE_RESGROUP:
                this.setState(prevState => {
                    return { resGroup: value };
                });
                break;
            case Actions.CHANGE_RESOURCE:
                this.setState(prevState => {
                    return { resource: value };
                });
                break;
            default:
                return;
        }
    }
    
    render() {
        return <div className="jumbotron h-100">
            <div className="text-secondary my-3">Choose language, resource group and resource name below to fetch the localized resource</div>
            <div className="form-group">
                <div className="row">
                    <div className="col-3 mx-1">
                        <LangSelector languages={Languages} currentLang={this.state.language} onSelect={this.onUpdate} />
                    </div>
                    <input type="text" value={this.state.resGroup} autoComplete="off" className="col-3 mx-1"
                        placeholder="Resource Group"
                        onChange={(evt) => this.onUpdate(Actions.CHANGE_RESGROUP, evt.target.value)} />
                    <input type="text" value={this.state.resource} autoComplete="off" className="col-3 mx-1"
                        placeholder="Resource"
                        onChange={(evt) => this.onUpdate(Actions.CHANGE_RESOURCE, evt.target.value)} />
                </div>
            </div >
            {this.state.resultSuccess && <div>
                <h5 className="mx-2 text-secondary">Resource found</h5>
                <div className="text-primary display-3">
                    RESULT
            </div>
            </div>
            }
            {this.state.resultFailure && <div>
                <h5 className="mx-2 text-danger">Resource not found</h5>
            </div>
            }
        </div>;
    }
}

export default Translator;