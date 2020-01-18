import axios from 'axios';

const http = axios.create({
    headers: { 'Content-Type': 'application/json' },
});

export async function getById(riverId: string) {
    const response = await http.get(`https://waterservices.usgs.gov/nwis/iv/?format=json&period=PT4H&siteStatus=all&sites=${riverId}`);
    return response.data;
}
