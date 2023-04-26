import { getFilesInAllSubDirectories, getPostBySlug } from './api';

describe('getFilesInDirectory', () => {
    it('should return an array of filenames', () => {
        const result = getFilesInAllSubDirectories('./_posts');
        expect(result).toContain('bijections.md');
        expect(result).toContain('2023/04/hello-world.md');
    });

});

describe('getPostBySlug', () => {

    const fields = ['slug', 'title', 'date', 'slug'];

    it('should be able to fetch a simple post', () => {
        const result = getPostBySlug('bijections', fields);
        expect(result).toEqual({
            slug: 'bijections',
            title: 'Why you should care about pairing functions?',
            date: '2020-03-16T05:35:07.322Z'
        });
    });

    it('should be able to fetch a post in a subdirectory', () => {
        const result = getPostBySlug('2023/04/hello-world', fields);
        expect(result).toEqual({
            slug: '2023/04/hello-world',
            title: 'Using Next.js for my Github Pages Blog',
            date: '2020-03-16T05:35:07.322Z'
        });
    });

});