import { ReactNode } from "react";
import Head from "next/head";
import ResumeLayout from "../components/resume-layout";

type Resume = {
    name: string;
    bio: {
        phone: string;
        email: string;
        location: string;
    },
    summary: string;
    experience: JobPosition[];
    communityEngagement: CommunityEngagement[];
    links: Link[];
    skills: Skill[];
}

type JobPosition = {
    company: string;
    location: string;
    position: string;
    startDate: string;
    endDate: string;
    bullets: string[];
    technologies?: string[];
}

type CommunityEngagement = {
    organization: string;
    position: string;
    startDate: string;
    endDate?: string;
    location: string;
    bullets: string[];
}

type Skill = {
    name: string;
}

type Link = {
    name: string;
    url: string;
}

export default function MyStaticResume() {
    return <>
        <ResumeLayout>
            <Head>
                <title>Steve Goguen - Software Engineer</title>
            </Head>
            <Resume resume={ResumeData()} />
        </ResumeLayout>
    </>


}

function JobPosition(props: { position: JobPosition }) {
    const p = props.position;
    return (
        <div>
            <p className="date">{p.startDate} - {p.endDate}</p>
            <div className="job_data">
                <h3>{p.company}</h3>
                <p className="location">{p.location}</p>
                <p className="position">{p.position}</p>
                <ul>
                    {p.bullets.map(b => <li className="tracking-tight leading-4">{b}</li>)}
                </ul>
            </div>
        </div>
    )
}



type ResumeProps = {
    title: string;
    // name: string;
    children: ReactNode;
}

function Resume(props: { resume: Resume }) {
    const r = props.resume;
    return <div>
        {/* Use Tailwind */}
        <h1>{r.name}</h1>
        <div id="bio">
            <p id="bio_left">
                {r.bio.phone}<br />
                {r.bio.email}
            </p>
            {/* Align right */}
            <p id="bio_right">
                {r.bio.location}
            </p>
        </div>

        {/* Add margin above */}
        <h2>SUMMARY</h2>
        <p className="data tracking-tighter leading-4 box-content text-justify">
            {r.summary}
        </p>

        <h2>EXPERIENCE</h2>

        {r.experience.map((e, i) => <JobPosition key={i} position={e} />)}

        <h2>COMMUNITY ENGAGEMENT</h2>

        {r.communityEngagement.map((e, i) => <div className="job" key={i}>
            <p className="date">{e.startDate} - {e.endDate}</p>
            <div className="job_data">
                <h3>{e.position}</h3>
                <p className="location">{e.location}</p>
                <p className="group">{e.organization}</p>
                <p className="position">{e.position}</p>
            </div>
        </div>)}


        {/* <h2>LINKS</h2>
        <table>
            <tbody>
                {r.links.map((l, i) => <tr key={i}>
                    <td><b>{l.name}</b></td>
                    <td><a href="{l.url}">{l.url}</a></td>
                </tr>)}
            </tbody>
        </table> */}

        <h2>KEYWORDS</h2>
        <ul id="skills">
            {r.skills.map((s, i) => <li key={i}>{s.name}</li>)}
        </ul>

    </div>
}

function ResumeData(): Resume {
    return {
        name: "Stephen Goguen",
        bio: {
            phone: "973-557-7267",
            email: "sgoguen@gmail.com",
            location: "Ashburn, VA",
        },
        summary: `Software engineer and team leader with over 25 years of experience in software development, team
                leadership, and project management. Passionate about building great teams and empowering individuals
                with good tools, great habit and empathy to deliver high-quality software that meets customer needs.`,
        experience: [
            {
                company: "Beacon Roofing Supply",
                location: "Herndon, VA",
                position: "IT Team Lead",
                startDate: "January 2018",
                endDate: "May 2023",
                bullets: [
                    `Launched www.becn.com and led a team of front-end developers to designed & build the new B2B eCommerce front-end written in Angular and TypeScript.`,
                    // `Implemented a catalog, quoting system, template system and manufacturer rebate tracking system`,
                    `Built and managed Google Cloud Platform environment, Firestore, Cloud Run, and pub/sub pipelines to ensure high availability and scalability.`,
                    `Developed a delivery tracking system with real-time tracking, notifications, and delivery
                    confirmation photos, improving customer satisfaction and reducing support inquiries using ASP.NET, Blazor, C#, SQL Server, Firestore, and Angular.`,
                    `Spearheaded the integration of Google's Document AI to automate the entry of purchase orders and human-in-the-loop UI for sales reps to
                    review and approve orders for key national accounts, reducing processing time and improving accuracy using Document AI, Python, NextJS, and Firestore`,
                ],
                technologies: ['Angular', 'TypeScript', 'C#', 'ASP.NET', 'Blazor', 'Python', 'NextJS', 'Firestore', 'Cloud Run', 'Google Cloud Platform']
            },
            {
                company: 'Allied Building Products',
                location: 'East Rutherford, NJ',
                position: 'Senior Software Engineer',
                startDate: 'September 2014',
                endDate: 'January 2018',
                bullets: [
                    `Developed & Launched Allied EDGE mobile platform allowing customers to place and track their
                    orders from their desktop, tablet or phone using ASP.NET, C#, SQL Server, and React and React Native`,
                    `Created Allied’s OnPoint delivery tracking system integrating Allied ERP system with Trimble’s
                    Transportation Management System and pushing delivery statuses and onsite photographs to our
                    eCommerce and mobile platform in near real-time`,
                    `Designed RESTful APIs to faciliate 3rd-party integrations for industry-leading CRM systems, EagleView, AccuLynx, JobNimbus`
                ],
                technologies: ['React', 'React Native', 'C#', 'ASP.NET', 'SQL Server']
            },
            {
                company: 'SevOne',
                location: 'Wilmington, DE',
                position: 'Team Lead',
                startDate: 'March 2012',
                endDate: 'September 2014',
                bullets: [
                    `Led the Development Automation Team and was responsible for the release management, continuous build
                    systems, source code static analysis, ticketing systems, security scanning, and compute resource provisioning
                    for both development and QA teams`,
                    `Automated VMWare VSphere to assist in the provisioning of development and QA environments for large clusters of VMs`,
                    `Created a custom database migration framework that was responsible for upgrading large clusters of MySQL databases`,
                    `Collaborated with other development team leads to solve common development pain points and to make
                    sure all code submitted adhered to company standards prior to pushing to main branches`,
                    `Built custom testing & observability framework that allowing developers and QA teams to visually navigate 
                    the test cases & outcomes and explore the internal state of the application`,
                    `Built the company's demo system simulating a large network, allowing sales engineers to spin up a demo environment.`,

                ],
                technologies: ['PHP', 'MySQL', 'Gentoo', 'JavaScript', 'Jenkins', 'Jira', 'Confluence', 'Git', 'VMWare', 'Coverity', 'HP Fortify']
            },
            {
                company: 'Allied Building Products',
                location: 'East Rutherford, NJ',
                position: 'Senior Software Engineer',
                startDate: 'September 2008',
                endDate: 'September, 2012',
                bullets: [
                    `Upgraded www.alliedbuilding.com and the company's custom intranet from a classic ASP website to an ASP.NET MVC framework`,
                    `Built websites for the company's private label brand (www.tribuilt.com) and solar division`,
                    `Developed an XML-driven application builder allowing IT support to build and customize workflow-based applications for the company's intranet`,
                    `Built an intranet application to facilitate document management and helping the company to
                    consolidate their paper-based processes to electronic workflows, reducing the company's need by 150
                    positions`
                ],
                technologies: ['ASP.NET', 'VB.NET', 'C#', 'SQL Server', 'JavaScript', 'jQuery', 'Classic ASP', 'VBScript']
            },
            {
                company: 'General Office Environments, Inc.',
                location: 'Rochelle Park, NJ',
                position: 'Software Engineer',
                startDate: 'October 2000',
                endDate: 'September 2007',
                bullets: [
                    `Responsible for converting the company’s in-house ERP system from a VB6 desktop application into a VB.NET web
                    application with a SQL Server backend. Web client was built using ASP.NET and VB.NET with a rich DHTML front-end.`,
                    `Designed and implemented inventory management software for facilities managers`,
                    `Developed a custom print ticket system tailored to the needs for office furniture installations. The system
                    would print tickets, MS Office documents, drawings and other documents needed for the installation
                    team to complete their work.`
                ],
                technologies: ['ASP.NET', 'VB.NET', 'VB', 'C#', 'SQL Server', 'JavaScript', 'Classic ASP', 'VBScript']
            },
            {
                company: `NBS Card Technologies`,
                location: 'Paramus, NJ',
                position: 'Software Engineer',
                startDate: 'October 1998',
                endDate: 'September 2000',
                bullets: [
                    `Supported firmware on the Image Master line of card printers and embossers (C and 80186 assembler)`,
                    `Developed custom solution to audit the production of Employment Authorization Cards and track the
                    consumables for Immigration and Naturalization Services.`,
                    `Created help system for the company's card design/layout software.`
                ]
            },
            {
                company: 'Wurth USA',
                location: 'Ramsey, NJ',
                position: 'Software Developer Consultant',
                startDate: 'September 1998',
                endDate: 'December 1998',
                bullets: [
                    `Developed VB6 client for a remote ordering system for Wurth USA's sales team allowing team members
                    to enter orders and submit them via SMTP so they could be routed to Wurth's SAP system`,
                    `Implement system allowing members to download catalog updates to local Access database.`
                ]
            },
            {
                company: 'Sterling Commerce',
                position: 'Software Engineer',
                location: 'Wayne, PA',
                startDate: 'September 1997',
                endDate: 'April 1998',
                bullets: [
                    `Developed and maintained various modules for the flagship CD-ROM catalog software.`,
                    `Used Visual **C++** to port and refactor large portions of the 16-bit Windows API application to a 32-bit MFC application.`,
                    `Rewrote installation program in InstallShield to handle over 90 very unique distributions of CD-ROM catalogs.`
                ]
            },
            {
                company: 'MS INet',
                location: 'Parsippany, NJ',
                position: 'Software Engineer',
                startDate: 'March 1997',
                endDate: 'July 1997',
                bullets: [
                    `Developed a TCP/IP messaging server using Visual C++ to facilitate direct communication between customers and NYSE traders on the floor.`,
                    `Implemented a message logging system to record all transactions to a **MS SQL Server** database.`,
                    `Utilized overlapping IO and IO completion ports for maximum efficiency.`,
                ]
            },
            {
                company: 'Integrated Quality Dynamics, Inc',
                location: 'Wilmington, DE',
                position: 'Software Developer',
                startDate: 'March 1996',
                endDate: 'March 1997',
                bullets: [
                    `Developed a suite of TQM software including tools for conceptualizing and planning projects, functional analysis, and project deployment.`,
                    `Created diagramming tools using Visual **C++** and designed a plug-in architecture for easy 3rd party integration and upgrades.`,
                    `Implemented OLE Document and Automation Server to embed diagrams in Word and Excel, and supported 3rd party scripting from Visual Basic.`
                ]
            }
        ],
        communityEngagement: [
            {
                organization: 'New York City F# User Group',
                position: 'Co-organizer',
                startDate: 'November 2014',
                endDate: 'December 2017',
                location: 'New York, NY',
                bullets: []
            },
            {
                organization: 'Southern Fried F# Conference',
                position: 'Presenter',
                startDate: 'April 2019',
                location: 'Raleigh, NC',
                bullets: []
            }
        ],
        links: [
            {
                name: 'Github',
                url: 'https://sgoguen.github.io/'
            },
            {
                name: 'Blog',
                url: 'https://sgoguen.github.io/blog/'
            },
        ],
        skills: [
            { name: 'C#' },
            { name: 'F#' },
            { name: '.NET' },
            { name: 'ASP.NET' },
            { name: 'TypeScript' },
            { name: 'JavaScript' },
            { name: 'React' },
            { name: 'Angular' },
            { name: 'Node.js' },
            { name: 'SQL Server' },
            { name: 'MongoDB' },
            { name: 'Docker' },
            { name: 'Firestore' },
            { name: 'Azure' },
            { name: 'Google Cloud Platform' },
            { name: 'C++' }

        ],
    }
}
