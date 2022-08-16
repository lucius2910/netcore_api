--
-- PostgreSQL database dump
--

-- Dumped from database version 14.1
-- Dumped by pg_dump version 14.2

-- Started on 2022-08-16 20:07:29

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 16499)
-- Name: public; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA public;


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 209 (class 1259 OID 16501)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


--
-- TOC entry 210 (class 1259 OID 16506)
-- Name: function; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.function (
    id uuid NOT NULL,
    module character varying(50),
    code character varying(50) NOT NULL,
    name character varying(250) NOT NULL,
    description character varying(500),
    url character varying(250),
    path character varying(250),
    method character varying(10),
    parent_cd text DEFAULT ''::text NOT NULL,
    "order" integer,
    is_active boolean NOT NULL,
    icon text,
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 211 (class 1259 OID 16520)
-- Name: log_action; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.log_action (
    id uuid NOT NULL,
    path character varying(250),
    method character varying(10),
    body text,
    message text,
    ip character varying(30),
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 212 (class 1259 OID 16527)
-- Name: log_exception; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.log_exception (
    id uuid NOT NULL,
    function character varying(250),
    message text,
    stack_trace text,
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 213 (class 1259 OID 16534)
-- Name: master_code; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.master_code (
    id uuid NOT NULL,
    type character varying(100) NOT NULL,
    key character varying(200) NOT NULL,
    value text NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 217 (class 1259 OID 16562)
-- Name: permission; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.permission (
    id uuid NOT NULL,
    role_cd character varying(15) NOT NULL,
    function_cd character varying(15) NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 214 (class 1259 OID 16541)
-- Name: resource; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.resource (
    id uuid NOT NULL,
    lang character varying(10) NOT NULL,
    module character varying(100) NOT NULL,
    screen character varying(100) NOT NULL,
    key character varying(200) NOT NULL,
    text text NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 215 (class 1259 OID 16548)
-- Name: role; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.role (
    id uuid NOT NULL,
    code character varying(15) NOT NULL,
    name character varying(160) NOT NULL,
    description character varying(240),
    is_actived character varying(1) NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 216 (class 1259 OID 16555)
-- Name: seq; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.seq (
    id uuid NOT NULL,
    code text NOT NULL,
    no integer NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 218 (class 1259 OID 16577)
-- Name: user; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."user" (
    id uuid NOT NULL,
    code character varying(15) NOT NULL,
    full_name character varying(100) NOT NULL,
    user_name character varying(100) NOT NULL,
    hashpass character varying(100),
    salt character varying(100),
    mail character varying(100) NOT NULL,
    phone character varying(15),
    is_actived boolean,
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 220 (class 1259 OID 16616)
-- Name: user_role; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.user_role (
    id uuid NOT NULL,
    user_cd character varying(15) NOT NULL,
    role_cd character varying(15) NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 219 (class 1259 OID 16591)
-- Name: user_token; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.user_token (
    id uuid NOT NULL,
    user_cd character varying(15) NOT NULL,
    access_token character varying(500) NOT NULL,
    refresh_token character varying(500) NOT NULL,
    access_token_expired_date date NOT NULL,
    refresh_token_expired_date date NOT NULL,
    ip character varying(20) NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_at timestamp without time zone,
    updated_by uuid,
    del_flg boolean NOT NULL
);


--
-- TOC entry 3392 (class 0 OID 16501)
-- Dependencies: 209
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."__EFMigrationsHistory" VALUES ('20220816123443_init_databse', '6.0.4');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20220816124102_update_functtion_fk', '6.0.4');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20220816130545_update_user_role', '6.0.4');


--
-- TOC entry 3393 (class 0 OID 16506)
-- Dependencies: 210
-- Data for Name: function; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3394 (class 0 OID 16520)
-- Dependencies: 211
-- Data for Name: log_action; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3395 (class 0 OID 16527)
-- Dependencies: 212
-- Data for Name: log_exception; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3396 (class 0 OID 16534)
-- Dependencies: 213
-- Data for Name: master_code; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3400 (class 0 OID 16562)
-- Dependencies: 217
-- Data for Name: permission; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3397 (class 0 OID 16541)
-- Dependencies: 214
-- Data for Name: resource; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3398 (class 0 OID 16548)
-- Dependencies: 215
-- Data for Name: role; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3399 (class 0 OID 16555)
-- Dependencies: 216
-- Data for Name: seq; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3401 (class 0 OID 16577)
-- Dependencies: 218
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3403 (class 0 OID 16616)
-- Dependencies: 220
-- Data for Name: user_role; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3402 (class 0 OID 16591)
-- Dependencies: 219
-- Data for Name: user_token; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 3213 (class 2606 OID 16514)
-- Name: function AK_function_code; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.function
    ADD CONSTRAINT "AK_function_code" UNIQUE (code);


--
-- TOC entry 3215 (class 2606 OID 16610)
-- Name: function AK_function_parent_cd; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.function
    ADD CONSTRAINT "AK_function_parent_cd" UNIQUE (parent_cd);


--
-- TOC entry 3227 (class 2606 OID 16554)
-- Name: role AK_role_code; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT "AK_role_code" UNIQUE (code);


--
-- TOC entry 3237 (class 2606 OID 16585)
-- Name: user AK_user_code; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "AK_user_code" UNIQUE (code);


--
-- TOC entry 3211 (class 2606 OID 16505)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3217 (class 2606 OID 16512)
-- Name: function PK_function; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.function
    ADD CONSTRAINT "PK_function" PRIMARY KEY (id);


--
-- TOC entry 3219 (class 2606 OID 16526)
-- Name: log_action PK_log_action; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.log_action
    ADD CONSTRAINT "PK_log_action" PRIMARY KEY (id);


--
-- TOC entry 3221 (class 2606 OID 16533)
-- Name: log_exception PK_log_exception; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.log_exception
    ADD CONSTRAINT "PK_log_exception" PRIMARY KEY (id);


--
-- TOC entry 3223 (class 2606 OID 16540)
-- Name: master_code PK_master_code; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.master_code
    ADD CONSTRAINT "PK_master_code" PRIMARY KEY (id);


--
-- TOC entry 3235 (class 2606 OID 16566)
-- Name: permission PK_permission; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.permission
    ADD CONSTRAINT "PK_permission" PRIMARY KEY (id);


--
-- TOC entry 3225 (class 2606 OID 16547)
-- Name: resource PK_resource; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.resource
    ADD CONSTRAINT "PK_resource" PRIMARY KEY (id);


--
-- TOC entry 3229 (class 2606 OID 16552)
-- Name: role PK_role; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT "PK_role" PRIMARY KEY (id);


--
-- TOC entry 3231 (class 2606 OID 16561)
-- Name: seq PK_seq; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.seq
    ADD CONSTRAINT "PK_seq" PRIMARY KEY (id);


--
-- TOC entry 3239 (class 2606 OID 16583)
-- Name: user PK_user; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "PK_user" PRIMARY KEY (id);


--
-- TOC entry 3246 (class 2606 OID 16620)
-- Name: user_role PK_user_role; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.user_role
    ADD CONSTRAINT "PK_user_role" PRIMARY KEY (id);


--
-- TOC entry 3242 (class 2606 OID 16597)
-- Name: user_token PK_user_token; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.user_token
    ADD CONSTRAINT "PK_user_token" PRIMARY KEY (id);


--
-- TOC entry 3232 (class 1259 OID 16604)
-- Name: IX_permission_function_cd; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_permission_function_cd" ON public.permission USING btree (function_cd);


--
-- TOC entry 3233 (class 1259 OID 16605)
-- Name: IX_permission_role_cd; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_permission_role_cd" ON public.permission USING btree (role_cd);


--
-- TOC entry 3243 (class 1259 OID 16631)
-- Name: IX_user_role_role_cd; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_user_role_role_cd" ON public.user_role USING btree (role_cd);


--
-- TOC entry 3244 (class 1259 OID 16632)
-- Name: IX_user_role_user_cd; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_user_role_user_cd" ON public.user_role USING btree (user_cd);


--
-- TOC entry 3240 (class 1259 OID 16607)
-- Name: IX_user_token_user_cd; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_user_token_user_cd" ON public.user_token USING btree (user_cd);


--
-- TOC entry 3247 (class 2606 OID 16611)
-- Name: function FK_function_function_code; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.function
    ADD CONSTRAINT "FK_function_function_code" FOREIGN KEY (code) REFERENCES public.function(parent_cd) ON DELETE RESTRICT;


--
-- TOC entry 3248 (class 2606 OID 16567)
-- Name: permission FK_permission_function_function_cd; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.permission
    ADD CONSTRAINT "FK_permission_function_function_cd" FOREIGN KEY (function_cd) REFERENCES public.function(code) ON DELETE RESTRICT;


--
-- TOC entry 3249 (class 2606 OID 16572)
-- Name: permission FK_permission_role_role_cd; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.permission
    ADD CONSTRAINT "FK_permission_role_role_cd" FOREIGN KEY (role_cd) REFERENCES public.role(code) ON DELETE RESTRICT;


--
-- TOC entry 3251 (class 2606 OID 16621)
-- Name: user_role FK_user_role_role_role_cd; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.user_role
    ADD CONSTRAINT "FK_user_role_role_role_cd" FOREIGN KEY (role_cd) REFERENCES public.role(code) ON DELETE RESTRICT;


--
-- TOC entry 3252 (class 2606 OID 16626)
-- Name: user_role FK_user_role_user_user_cd; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.user_role
    ADD CONSTRAINT "FK_user_role_user_user_cd" FOREIGN KEY (user_cd) REFERENCES public."user"(code) ON DELETE RESTRICT;


--
-- TOC entry 3250 (class 2606 OID 16598)
-- Name: user_token FK_user_token_user_user_cd; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.user_token
    ADD CONSTRAINT "FK_user_token_user_user_cd" FOREIGN KEY (user_cd) REFERENCES public."user"(code) ON DELETE RESTRICT;


-- Completed on 2022-08-16 20:07:30

--
-- PostgreSQL database dump complete
--

