CREATE TABLE IF NOT EXISTS public.categories
(
    id uuid NOT NULL,
    category_name character varying(50)  NOT NULL,
    CONSTRAINT primarykey_of_categories PRIMARY KEY (id)
); 
