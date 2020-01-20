import axios from 'axios';

const http = axios.create({
    baseURL: 'https://waterservices.usgs.gov/nwis/iv/',
    headers: { 'Content-Type': 'application/json' },
});

const urlParams = '&format=json&period=PT4H&siteStatus=all';

export async function getById(riverId: string) {
    const response = await http.get(`?sites=${riverId}` + urlParams);
    return response.data;
}
