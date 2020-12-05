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

    fetchResource() {
        if (this.state.language) {
            fetch(format(TranslatorApi, this.state.language.shortName, this.state.resGroup, this.state.resource))
                .then(response => {
                    if (response.ok) { return response.text(); }
                    throw new Error(response.text());
                })
                .then(data => this.setState({ result: data, resultSuccess: true, resultFailure: false }))
                .catch(err => {
                    this.setState({ result: 'Resource not found', resultFailure: true, resultSuccess: false })
                });
        }
        else {
            this.setState({ result: 'Choose a language', resultSuccess: false, resultFailure: true  })
        }
    }

    render() {
        return <div className="jumbotron h-100 ">
            <div className="text-secondary my-3 container">Choose language, resource group and resource name below to fetch the localized resource</div>
            <div className="form-group container">
                <div className="row">
                    <div className="col-sm-3 col-xs-6 mr-1 my-1">
                        <LangSelector languages={Languages} currentLang={this.state.language} onSelect={this.onUpdate} />
                    </div>
                    <input type="text" value={this.state.resGroup} autoComplete="off" className="col-sm-3 col-xs-6 mr-1 my-1"
                        placeholder="Resource Group"
                        onChange={(evt) => this.onUpdate(Actions.CHANGE_RESGROUP, evt.target.value)} />
                    <input type="text" value={this.state.resource} autoComplete="off" className="col-sm-3 col-xs-6  mr-1 my-1"
                        placeholder="Resource"
                        onChange={(evt) => this.onUpdate(Actions.CHANGE_RESOURCE, evt.target.value)} />
                    <input type="button" value="Get resource" onClick={this.fetchResource.bind(this)} className="col-sm-2 col-xs-6 my-1 btn-primary" />
                </div>
            </div>
            {this.state.resultSuccess && <div className="container">
                <h5 className="mx-2 text-secondary">Resource found</h5>
                <div className="text-primary display-3">
                    {this.state.result}
                </div>
            </div>
            }
            {this.state.resultFailure && <div className="container">
                <h5 className="mx-2 text-danger">{this.state.result}</h5>
            </div>
            }
        </div>;
    }
}

export default Translator;