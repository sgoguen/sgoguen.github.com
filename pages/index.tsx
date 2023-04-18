import Container from '../components/container'
import MoreStories from '../components/more-stories'
import HeroPost from '../components/hero-post'
import Intro from '../components/intro'
import Layout from '../components/layout'
import { getAllPosts } from '../lib/api'
import Head from 'next/head'
import { CMS_NAME } from '../lib/constants'
import Post from '../interfaces/post'

type Props = {
  allPosts: Post[]
}

export default function Index({ allPosts }: Props) {
  const heroPost = allPosts[0]
  const morePosts = allPosts.slice(1)
  return (
    <>
      <Layout>
        <Head>
          <title>Steve Goguen's Blog</title>
        </Head>
        <Container>
          <Intro />

          {/* Presentations */}
          <h2 className="mb-8 text-5xl md:text-4xl font-bold tracking-tighter leading-tight">
            Presentations
          </h2>

          <div className="grid grid-cols-1 md:grid-cols-2 md:gap-x-16 lg:gap-x-32 gap-y-20 md:gap-y-32 mb-32">
            <div className="flex flex-col justify-center">
              <h3 className="text-3xl md:text-4xl font-bold tracking-tighter leading-tight">
                <a
                  href="https://www.youtube.com/watch?v=QZVYP3cPcWQ"
                  className="hover:underline"
                >
                  How to Build a Serverless GraphQL API with AWS Amplify
                </a>
              </h3>
              <div className="text-lg mt-3">
                <a
                  href="https://www.youtube.com/watch?v=QZVYP3cPcWQ"
                  className="text-blue-600 hover:underline"
                >
                  Watch on YouTube
                </a>
              </div>
            </div>
            <div className="flex flex-col justify-center">
              <h3 className="text-3xl md:text-4xl font-bold tracking-tighter leading-tight">
                <a
                  href="https://www.youtube.com/watch?v=QZVYP3cPcWQ"
                  className="hover:underline"
                >
                  How to Build a Serverless GraphQL API with AWS Amplify
                </a>
              </h3>
              <div className="text-lg mt-3">
                <a
                  href="https://www.youtube.com/watch?v=QZVYP3cPcWQ"
                  className="text-blue-600 hover:underline"
                >
                  Watch on YouTube
                </a>
              </div>
            </div>
          </div>

          {/* {heroPost && (
            <HeroPost
              title={heroPost.title}
              coverImage={heroPost.coverImage}
              date={heroPost.date}
              author={heroPost.author}
              slug={heroPost.slug}
              excerpt={heroPost.excerpt}
            />
          )} */}

          {/* Blog Stories */}
          {morePosts.length > 0 && <MoreStories posts={morePosts} />}
        </Container>
      </Layout>
    </>
  )
}

export const getStaticProps = async () => {
  const allPosts = getAllPosts([
    'title',
    'date',
    'slug',
    'author',
    'coverImage',
    'excerpt',
  ])

  return {
    props: { allPosts },
  }
}
