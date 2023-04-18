import { useRouter } from 'next/router'
import ErrorPage from 'next/error'
import Container from '../../components/container'
import PostBody from '../../components/post-body'
import Header from '../../components/header'
import PostHeader from '../../components/post-header'
import Layout from '../../components/layout'
import { getPostBySlug, getAllPosts } from '../../lib/api'
import PostTitle from '../../components/post-title'
import Head from 'next/head'
import { CMS_NAME } from '../../lib/constants'
import markdownToHtml from '../../lib/markdownToHtml'
import PostType from '../../types/post'

type Props = {
  note: unknown
//   morePosts: PostType[]
//   preview?: boolean
}

const Note = ({  note }: Props) => {
  const router = useRouter()
//   if (!router.isFallback && !post?.slug) {
//     return <ErrorPage statusCode={404} />
//   }
  return (
    <Layout preview={false}>
      <Container>
          <h1>Title</h1>
          <p>Hello World</p>
          <pre>{JSON.stringify(note)}</pre>
      </Container>
    </Layout>
  )
}

export default Note

type Params = {
  params: {
    slug: string
  }
}

export async function getStaticProps({ params }: Params) {
    const slug = params.slug;
    const note = notes[slug];
//   const post = getPostBySlug(params.slug, [
//     'title',
//     'date',
//     'slug',
//     'author',
//     'content',
//     'ogImage',
//     'coverImage',
//   ])
//   const content = await markdownToHtml(post.content || '')

  return {
    props: {
        slug: slug,
        note: note,
    },
  }
}

const notes: Record<string, unknown> = {
  'hello-world': 'Hello'
};

export async function getStaticPaths() {
//   const posts = getAllPosts(['slug'])


  return {
    paths: Object.entries(notes).map(([slug, content]) => {
      return {
        params: {
          slug: slug,
          content: content
        },
      }
    }),
    fallback: false,
  }
}
