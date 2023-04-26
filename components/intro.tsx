import { CMS_NAME } from '../lib/constants'

const Intro = () => {
  return (
    <>
      <section className="flex-col md:flex-row flex items-center md:justify-between mt-16 mb-16 md:mb-12">
        <h1 className="text-5xl md:text-6xl font-bold tracking-tighter leading-tight md:pr-8">
          Steve Goguen's Github Profile
        </h1>
        {/* <h4 className="text-center md:text-left text-lg mt-5 md:pl-8">
        A statically generated blog
      </h4> */}
        <p className="text-lg mt-5 md:pl-6">
          I'm a software developer who has all sorts of interests. I love to learn and teach.
          I'm a big fan of F#, TypeScript, the Fable compiler and programming languages in general.
        </p>
      </section>
    </>
  )
}

export default Intro
