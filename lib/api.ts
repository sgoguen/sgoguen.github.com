import fs from 'fs'
import { join } from 'path'
import matter from 'gray-matter'

const postsDirectory = join(process.cwd(), '_posts')

export function getFilesInAllSubDirectories(dir: string, appendBefore: string = "") {
  const directoryItems = fs.readdirSync(dir);
  const result: string[] = [];

  directoryItems.forEach((directoryItem) => {
      const fullPath = join(dir, directoryItem);
      const stat = fs.statSync(fullPath);
      if (stat.isDirectory()) {
          result.push(...getFilesInAllSubDirectories(fullPath, join(appendBefore, directoryItem)));
      } else {
          result.push(join(appendBefore, directoryItem));
      }
  });

  return result;
}


export function getPostSlugs() {
  const filesAndFolders = fs.readdirSync(postsDirectory);
  const files = filesAndFolders.filter((fileOrFolder) => {
    const isDirectory = fs.statSync(join(postsDirectory, fileOrFolder)).isDirectory();
    return !isDirectory;
  });


  //  Look in the posts directory and return the filenames in all the subdirectories
  //  but only return filenames
  return files;
  // return getFilesInAllSubDirectories(postsDirectory);
}
type Items = {
  [key: string]: string
}

export function getPostBySlug(slug: string, fields: string[] = []): Items {

  const realSlug = slug.replace(/\.md$/, '');
  const fullPath = join(postsDirectory, `${realSlug}.md`);
  const fileContents = fs.readFileSync(fullPath, 'utf8');
  const { data, content } = matter(fileContents);

  const items: Items = {}

  // Ensure only the minimal needed data is exposed
  fields.forEach((field) => {
    if (field === 'slug') {
      items[field] = realSlug
    }
    if (field === 'content') {
      items[field] = content
    }

    if (typeof data[field] !== 'undefined') {
      items[field] = data[field]
    }
  })

  return items
}

export function getAllPosts(fields: string[] = []) {
  const slugs = getPostSlugs()
  const posts = slugs
    .map((slug) => getPostBySlug(slug, fields))
    // sort posts by date in descending order
    .sort((post1, post2) => (post1.date > post2.date ? -1 : 1))
  return posts
}
