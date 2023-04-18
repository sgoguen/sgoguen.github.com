import * as z from 'zod';

const tabsType = z.object({
    '@type': z.literal('tabs'),
    tabs: z.record(z.unknown())
});

type Tabs = z.infer<typeof tabsType>;