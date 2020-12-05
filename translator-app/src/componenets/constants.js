export const Actions = {
    CHANGE_LANG: "change_lang",
    CHANGE_RESGROUP: "change_resourceGroup",
    CHANGE_RESOURCE: "change_resource"
};

export const Languages = [
    { shortName: 'en', fullName: 'English' },
    { shortName: 'fi', fullName: 'Finnish' }
];

export const TranslatorApi = `https://anas-abb-translatorapi.azurewebsites.net/api/translation/{0}/{1}_{2}?code=6luEacFCOwTZ7U5facVprlemeHnCAbl0FiAl9q0Je25QXr7KQDeCWg==`