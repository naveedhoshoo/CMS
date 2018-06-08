using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ClaimManagement
{
    public class ClaimClass
    {
        private dal db;

        private int _claimIdx;
        public int claimIdx { get { return _claimIdx; } set { _claimIdx = value; } }

        private string _userIdx;
        public string userIdx { get { return _userIdx; } set { _userIdx = value; } }

        private string _Date;
        public string Date { get { return _Date; } set { _Date = value; } }
        private string _desc;
        public string desc { get { return _desc; } set { _desc = value; } }

        private int _holdIdx;
        public int holdIdx { get { return _holdIdx; } set { _holdIdx = value; } }

        private int _statusIdx;
        public int statusIdx { get { return _statusIdx; } set { _statusIdx = value; } }

        public void getClaimRecordDemo(DataTable dt)
        {
            db = new dal();
            string qry = @"SELECT DISTINCT piydesc,INTIMATIONNO,PPS_PARTY_CODE,INSURED,BR_CODE,BR_DESC,PDP_CODE,PDP_DESC,gih_doc_ref_no, gih_year,sum(TOTALLOSS) totalloss ,sum(TOTALPAID) totalpaid,GIC_SHARE,(sum(TOTALLOSS)*GIC_SHARE/100)-(sum(TOTALPAID)*GIC_SHARE/100)os
        FROM (
        Select distinct
         h.piy_desc as piydesc,
        h.GIS_REGISTRATION_NO,    
        h.GIS_ENGINE_NO,   H.GIS_SAILING_DATE, coalesce(H.coinsurere,'x') coinsurere,
        h.GIS_CHASSIS_NO, 
        h.bank,
        H.PLC_LOC_CODE AS BR_CODE,
        H.BRANCH AS BR_DESC,h.piy_desc,
        H.PDP_DEPT_CODE AS PDP_CODE,
        H.DEPARTMENT AS PDP_DESC,H.GIH_OLDCLAIM_NO,
        h.gih_doc_ref_no,h.gih_documentno, h.gih_year,
        H.INTIMATIONNO AS INTIMATIONNO,
        H.GIH_DATEOFLOSS AS DATEOFLOSS,
        H.POC_LOSSDESC AS LOSSDESC, h.psr_surv_name,
        H.GID_BASEDOCUMENTNO AS BASEDOCNO,H.PPS_PARTY_CODE,
        H.INSURED AS INSURED,H.REMARKS,H.PLACE,
        H.GID_COMMDATE AS COMMDATE, 
        coalesce( H.GIS_SUMINSURED,0) AS SUMINSURED, 
        coalesce (H.TOTLOSS,0) AS TOTALLOSS, 
        coalesce (H.LOSSPAY,0) AS LOSSPAY, 
        coalesce( H.SURYORPAY,0) AS SURVPAY, 
        coalesce (H.ADVOCATEPAY,0) AS ADVOCATEPAY,
        coalesce (SALVAGE,0) AS SALVAGE,
        COALESCE(H.TOTALPAID,0) TOTALPAID,
        H.GIH_INTIMATIONDATE AS INTIMATIONDATE,h.rev_date,
        H.GID_EXPIRYDATE AS EXPIRY  ,coalesce(h.gic_share,100)gic_share

        from (
        ( 
        select  
        IH.PLC_LOC_CODE        AS PLC_LOC_CODE, bnk.pbn_bnk_desc bank,
        LC.PLC_DESC            AS BRANCH, 
        DP.PDP_DESC            AS DEPARTMENT,    
        IH.GIH_INTIMATIONDATE  AS GIH_INTIMATIONDATE,   ih.gih_revisiondate rev_date,
        IH.PDP_DEPT_CODE       AS PDP_DEPT_CODE, IH.GIH_REMARKS REMARKS,
        IH.GIH_PLACEOFTHEFT PLACE,
        IH.POR_ORG_CODE        AS ORG,  ih.gih_year,
        IH.GIH_DOC_REF_NO,ih.gih_documentno,IH.GIH_OLDCLAIM_NO,
        IH.GIH_INTI_ENTRYNO    AS IntimationNo,  
        --iH.PIY_INSUTYPE,
        ID.GID_BASEDOCUMENTNO  AS GID_BASEDOCUMENTNO,  
        IH.GIH_DATEOFLOSS      AS GIH_DATEOFLOSS, sv.psr_surv_name,
        OC.POC_LOSSDESC        AS POC_LOSSDESC,   
        ID.GID_COMMDATE        AS GID_COMMDATE,  
        ID.GID_EXPIRYDATE      AS GID_EXPIRYDATE, coalesce(ic.gic_share,100)gic_share,
        piy.piy_desc,  ISUBDTL.GIS_REGISTRATION_NO,    
        ISUBDTL.GIS_ENGINE_NO,          
        ISUBDTL.GIS_CHASSIS_NO,  ISUBDTL.GIS_SAILING_DATE,
        IH.PPS_PARTY_CODE,       
        INS.PPS_DESC  AS INSURED, 
        coi.pps_desc coinsurere,
        SUM(coalesce(ISUBDTL.GIS_SUMINSURED,0))   AS GIS_SUMINSURED,  
        SUM(coalesce(IH.GIH_LOSSCLAIMED,0)) AS GSD_LOSSCLAIMED,  
        SUM(coalesce(ISUBDTL.GIS_SURVEYORAMT,0))  AS SURYORAMT,  
        SUM(coalesce(ISUBDTL.GIS_ADVOCATEAMT,0))  AS ADVORAMT,
 
        SUM(CASE coalesce(ISUBDTL.GIS_LOSSADJUSTED, 0)  WHEN 0 THEN  CASE coalesce(ISUBDTL.GIS_LOSSASSESSED, 0)  
                      WHEN 0 THEN  CASE coalesce(ISUBDTL.GIS_LOSSCLAIMED, 0)  WHEN 0 THEN 0  ELSE coalesce(ISUBDTL.GIS_LOSSCLAIMED, 0)  END  
                      ELSE coalesce (ISUBDTL.GIS_LOSSASSESSED, 0)  END  
        ELSE coalesce(ISUBDTL.gIS_LOSSADJUSTED, 0)  
        END * CASE  WHEN GIH_NOCLAIM_TAG = 'N' THEN 0 ELSE 1 END 
        + coalesce(ISUBDTL.GIS_SURVEYORAMT, 0) + coalesce(ISUBDTL.GIS_ADVOCATEAMT, 0) - 
        CASE 'Y' WHEN 'Y' THEN coalesce(ISUBDTL.GIS_SALVAGEAMT,0) ELSE 0 END
        ) as TotLoss,
        LOSSPAY * CASE WHEN GIH_NOCLAIM_TAG ='N' THEN 0 ELSE 1 END AS LOSSPAY,    
        SURYORPAY   AS SURYORPAY,    
        ADVOCATEPAY AS ADVOCATEPAY, 
         (CASE 'Y' WHEN 'Y' THEN SALVAGE ELSE 0 END) AS SALVAGE,
        LOSSPAY + SURYORPAY  + ADVOCATEPAY - (CASE 'Y' WHEN 'Y' THEN SALVAGE ELSE 0 END) as TotalPaid  

        FROM       

        GI_GC_IH_INTIMATIONHD IH 

        left outer join GI_GC_ID_INTIMATIONDTL ID  on (  
          IH.PDP_DEPT_CODE    =  ID.PDP_DEPT_CODE    AND      
          IH.POR_ORG_CODE     =  ID.POR_ORG_CODE     AND  
          IH.PLC_LOC_CODE     =  ID.PLC_LOC_CODE     AND        
          IH.PDT_DOCTYPE      =  ID.PDT_DOCTYPE      AND         
          IH.GIH_DOCUMENTNO   =  ID.GIH_DOCUMENTNO   AND      
          IH.GIH_INTI_ENTRYNO =  ID.GIH_INTI_ENTRYNO AND      
          IH.GIH_YEAR           =  ID.GIH_YEAR  )  

        left outer join GI_GC_IS_INTIMATIONSUBDTL ISUBDTL  on (  
          ID.POR_ORG_CODE     = ISUBDTL.POR_ORG_CODE   AND             
          ID.PLC_LOC_CODE     = ISUBDTL.PLC_LOC_CODE   AND              
          ID.PDP_DEPT_CODE    = ISUBDTL.PDP_DEPT_CODE  AND            
          ID.PDT_DOCTYPE      = ISUBDTL.PDT_DOCTYPE    AND               
          ID.GIH_DOCUMENTNO   = ISUBDTL.GIH_DOCUMENTNO AND          
          ID.GIH_INTI_ENTRYNO = ISUBDTL.GIH_INTI_ENTRYNO AND         
          ID.GIH_YEAR         = ISUBDTL.GIH_YEAR       AND                     
          ID.GID_PLY_SERIALNO = ISUBDTL.GID_PLY_SERIALNO )  
        left outer join GI_GC_IC_INTIMATION_COINSURER IC ON
        (
        ID.POR_ORG_CODE         = IC.POR_ORG_CODE     AND  
        ID.PLC_LOC_CODE         = IC.PLC_LOC_CODE     AND  
        ID.PDP_DEPT_CODE        = IC.PDP_DEPT_CODE    AND  
        ID.PDT_DOCTYPE          = IC.PDT_DOCTYPE         AND  
        ID.GIH_DOCUMENTNO       = IC.GIH_DOCUMENTNO   AND  
        ID.GIH_INTI_ENTRYNO     = IC.GIH_INTI_ENTRYNO AND  
        ID.GIH_YEAR             = IC.GIH_YEAR         AND  
        ID.GID_BASEDOCUMENTNO = IC.GIC_BASEDOCUMENTNO  AND 
        IC.GIC_CORETAG='C' AND IC.GIC_LEADERTAG='Y' 
        )
        left outer join pr_gn_ps_party coi on (coi.por_org_code=ih.por_org_code and coi.pps_party_code=ih.pps_insu_code)
        inner join  
        (select gih_doc_ref_no,max(gih_inti_entryno*1) ENTRYNO 
        from GI_GC_IH_INTIMATIONHD  where  --pdp_dept_code = '11'  and  
        case gih_inti_entryno when '1' then GIH_INTIMATIONDATE else GIH_REVISIONDATE end  BETWEEN   '01-Jan-2000'  AND '31-May-2018'
        and GIH_INTIMATIONDATE between '01-Jan-2000'  AND '31-May-2018'           
        group by gih_doc_ref_no) HD ON ( HD.gih_doc_ref_no=IH.gih_doc_ref_no AND ENTRYNO = IH.gih_inti_entryno ) 
 
        LEFT OUTER JOIN PR_GC_OC_LOSS_CAUSE OC ON (  
        IH.PDP_DEPT_CODE      = OC.PDP_DEPT_CODE       AND       
        IH.POC_LOSSCODE       = OC.POC_LOSSCODE   )  

        LEFT OUTER JOIN PR_GN_PS_PARTY INS ON     ( 
        IH.POR_ORG_CODE       =  INS.POR_ORG_CODE      AND  
        IH.PPS_PARTY_CODE   =  INS.PPS_PARTY_CODE )
 
        INNER JOIN PR_GN_DP_DEPARTMENT DP ON ( 
        IH.PLC_LOC_CODE  =  DP.PLC_LOC_CODE            AND  
        IH.PDP_DEPT_CODE = DP.PDP_DEPT_CODE  )
        left outer join GI_GC_UD_SURVEYORDTL svd on (
        IH.POR_ORG_CODE         = svd.POR_ORG_CODE     AND 
        IH.PLC_LOC_CODE          = svd.PLC_LOC_CODE     AND 
        IH.PDP_DEPT_CODE        = svd.PDP_DEPT_CODE    AND 
        IH.PDT_DOCTYPE            = svd.PDT_DOCTYPE      AND 
        IH.GIH_DOCUMENTNO     = svd.GIH_DOCUMENTNO   AND     
        IH.GIH_INTI_ENTRYNO    = svd.GIH_INTI_ENTRYNO AND 
        IH.GIH_YEAR                  = svd.GIH_YEAR and svd.gud_serialno=1) 
        left outer join PR_GG_SR_SURVEYOR sv on (
        svd.psr_surv_code=sv.psr_surv_code) 
        INNER JOIN PR_GN_LC_LOCATION LC ON  ( 
        IH.POR_ORG_CODE  =  LC.POR_ORG_CODE            AND  
        IH.PLC_LOC_CODE  =  LC.PLC_LOC_CODE )INNER JOIN
        PR_GG_IY_INSURANCETYPE PIY
        ON
        (
        iH.PIY_INSUTYPE=PIY.PIY_INSUTYPE
        )
        left outer join GI_GR_VC_VOYAGE_CARD vc 
        on
        (
        vc.GVC_POLICYNO = ID.GID_BASEDOCUMENTNO  and 
        vc.GVC_VOYAGECARD_NO = ID.GVC_VOYAGECARD_NO AND 
        vc.GVC_ITEMNO = ID.GID_ITEMNOS )
        INNER JOIN GI_GU_DH_DOC_HEADER DH ON (ID.GID_BASEDOCUMENTNO=DH.GDH_DOC_REFERENCE_NO AND DH.GDH_RECORD_TYPE='O')
        left outer join (
        select 
        BD.POR_ORG_CODE,BD.PLC_LOC_CODE,BD.PDP_DEPT_CODE,BD.PBC_BUSICLASS_CODE,BD.PIY_INSUTYPE,BD.PDT_DOCTYPE,   
        BD.GDH_DOCUMENTNO , BD.GDH_RECORD_TYPE,BD.GDH_YEAR,bn.PBN_BNK_DESC ,BD.PBN_BNK_CODE
        from GI_GU_BD_BANKDTL BD inner join PR_GN_BN_BANK BN on (
        BD.POR_ORG_CODE  =  BN.POR_ORG_CODE and
        BD.PBN_BNK_CODE  =  BN.PBN_BNK_CODE )
        ) bnk 
        on (
        dh.POR_ORG_CODE =  bnk.POR_ORG_CODE and
        dh.PLC_LOC_CODE  =  bnk.PLC_LOC_CODE   and
        dh.PDP_DEPT_CODE  =  bnk.PDP_DEPT_CODE   and
        dh.PBC_BUSICLASS_CODE =   bnk.PBC_BUSICLASS_CODE  and
        dh.PIY_INSUTYPE    =   bnk.PIY_INSUTYPE    and
        dh.PDT_DOCTYPE  =  bnk.PDT_DOCTYPE   and
        dh.GDH_DOCUMENTNO   =  bnk.GDH_DOCUMENTNO   and
        dh.GDH_RECORD_TYPE =  bnk.GDH_RECORD_TYPE and
        dh.GDH_YEAR    =    bnk.GDH_YEAR )
        LEFT OUTER JOIN (  
        select PD1.POR_ORG_CODE,PD1.PLC_LOC_CODE,PD1.PDP_DEPT_CODE,
        PD1.PDT_DOCTYPE,PD1.GIH_DOCUMENTNO,PD1.GIH_YEAR,PD1.GSD_SERIALNO,
        sum(LOSSPAY)     as LOSSPAY,
        sum(SURYORPAY)   as SURYORPAY,
        sum(ADVOCATEPAY) as ADVOCATEPAY, 
        sum(SALVAGE)     as SALVAGE 

        FROM         
        (select 
        SHD.POR_ORG_CODE,SHD.PLC_LOC_CODE,SHD.PDP_DEPT_CODE,SHD.PDT_DOCTYPE,
        SHD.GIH_DOCUMENTNO,SHD.GIH_INTI_ENTRYNO,SHD.GIH_YEAR,SHD.GSH_ENTRYNO, SDTL.GSD_SERIALNO,    
        CASE PYY_CODE WHEN '01' THEN (coalesce(GSD_LOSSPAID,0)) ELSE  0 END as LOSSPAY,        
        CASE PYY_CODE WHEN '02' THEN (coalesce(GSD_LOSSPAID,0)) ELSE  0 END as SURYORPAY,        
        CASE PYY_CODE WHEN '03' THEN (coalesce(GSD_LOSSPAID,0)) ELSE  0 END as ADVOCATEPAY, 
        CASE PYY_CODE WHEN '04' THEN (coalesce(GSD_LOSSPAID,0)) ELSE  0 END as SALVAGE,   
        coalesce(GSD_CONETAMOUNT,0)  net,
        coalesce(GSD_TREATYAMOUNT,0) treaty,
        coalesce(GSD_FACULTAMOUNT,0) fac,  
        coalesce(GSD_EXCESSAMOUNT,0) excess,
        coalesce(GSD_LOSSPAID,0)     PAID  
        FROM GI_GC_SH_SETTELMENTHD SHD 
        INNER JOIN GI_GC_SD_SETTELMENTDTL SDTL ON (            
                  SDTL.POR_ORG_CODE            = SHD.POR_ORG_CODE        AND       
                  SDTL.PLC_LOC_CODE            = SHD.PLC_LOC_CODE        AND       
                  SDTL.PDP_DEPT_CODE           = SHD.PDP_DEPT_CODE    AND       
                  SDTL.PDT_DOCTYPE             = SHD.PDT_DOCTYPE        AND       
                  SDTL.GIH_DOCUMENTNO          = SHD.GIH_DOCUMENTNO    AND       
                  SDTL.GIH_INTI_ENTRYNO        = SHD.GIH_INTI_ENTRYNO    AND       
                  SDTL.GIH_YEAR                = SHD.GIH_YEAR         AND      
                  SDTL.GSH_ENTRYNO             = SHD.GSH_ENTRYNO)
         
        WHERE SHD.GSH_SETTLEMENTDATE BETWEEN   SHD.GSH_SETTLEMENTDATE AND  '31-May-2018' 
        AND 
        coalesce(SHD.GSH_POSTINGTAG,'N') = 'Y') PD1  
        GROUP BY PD1.POR_ORG_CODE,PD1.PLC_LOC_CODE,PD1.PDP_DEPT_CODE,PD1.PDT_DOCTYPE,PD1.GIH_DOCUMENTNO,PD1.GIH_YEAR,PD1.GSD_SERIALNO 
        ) PD on ( 
            PD.POR_ORG_CODE            = IH.POR_ORG_CODE    AND    
            PD.PLC_LOC_CODE            = IH.PLC_LOC_CODE    AND    
            PD.PDP_DEPT_CODE           = IH.PDP_DEPT_CODE    AND    
            PD.PDT_DOCTYPE             = IH.PDT_DOCTYPE        AND    
            PD.GIH_DOCUMENTNO          = IH.GIH_DOCUMENTNO    AND    
            PD.GIH_YEAR                = IH.GIH_YEAR        AND  
            PD.GSD_SERIALNO            = ID.GID_PLY_SERIALNO ) 
        where      
          
        dh.GDH_COMMDATE   between '01-Jan-2000'  AND '31-May-2018'
       
        and  COALESCE(IH.GIH_CANCELLATIONTAG,'N') =CASE 'N' WHEN 'Y' THEN 'Y' when 'N' then 'N' else COALESCE(IH.GIH_CANCELLATIONTAG,'N') END  
          
        Group by  
        IH.PLC_LOC_CODE,LC.PLC_DESC,DP.PDP_DESC,IH.GIH_INTIMATIONDATE,    ih.gih_revisiondate,piy.piy_desc,
        IH.PDP_DEPT_CODE,IH.POR_ORG_CODE,  IH.GIH_INTI_ENTRYNO , IH.GIH_DOC_REF_NO, IH.GIH_OLDCLAIM_NO,
        IH.GIH_NOCLAIM_TAG,ID.GID_BASEDOCUMENTNO,IH.GIH_DATEOFLOSS, OC.POC_LOSSDESC,ISUBDTL.GIS_REGISTRATION_NO,    
        ISUBDTL.GIS_ENGINE_NO,   ISUBDTL.GIS_SAILING_DATE, coi.pps_desc,       
        ISUBDTL.GIS_CHASSIS_NO, bnk.pbn_bnk_desc,
        ID.GID_COMMDATE,ID.GID_EXPIRYDATE,ih.PPS_PARTY_CODE,INS.PPS_DESC,SURYORPAY,ADVOCATEPAY,SALVAGE,LOSSPAY,ih.gih_documentno, ih.gih_year,IH.GIH_REMARKS,IH.GIH_PLACEOFTHEFT,coalesce(ic.gic_share,100), sv.psr_surv_name
       
        ) ) H 
        ORDER BY  H.PLC_LOC_CODE ,H.PDP_DEPT_CODE , SUBSTRING (H.INTIMATIONNO,1,4) ,SUBSTRING (H.INTIMATIONNO, LEN (H.INTIMATIONNO)-7,8) 
        )    WHERE (TOTALLOSS*GIC_SHARE/100)-(TOTALPAID*GIC_SHARE/100) <>0
        group by piydesc,INTIMATIONNO,PPS_PARTY_CODE,INSURED,BR_CODE,BR_DESC,PDP_CODE,PDP_DESC,gih_doc_ref_no, gih_year,GIC_SHARE 
        ORDER BY (TOTALLOSS*GIC_SHARE/100)-(TOTALPAID*GIC_SHARE/100)        ";
                        db.getData(qry, dt);

        }



        public void getClaimRecordsByCriteria(DataTable dt, string ToDate = "", string IntimationNo = "", string brdescparam ="", string insuredparam="", string piydesc="")
        

        {
            //31-May-2018
            if (String.IsNullOrEmpty(ToDate))
            {
                ToDate = DateTime.Today.ToString("dd-MMM-yyy");
            }

            string qry = @"SELECT DISTINCT piydesc,INTIMATIONNO,PPS_PARTY_CODE,INSURED,BR_CODE,BR_DESC,PDP_CODE,PDP_DESC,gih_doc_ref_no, gih_year,sum(TOTALLOSS) totalloss ,sum(TOTALPAID) totalpaid,GIC_SHARE,(sum(TOTALLOSS)*GIC_SHARE/100)-(sum(TOTALPAID)*GIC_SHARE/100)os
                FROM (
                Select distinct
                 h.piy_desc as piydesc,
                h.GIS_REGISTRATION_NO,    
                h.GIS_ENGINE_NO,   H.GIS_SAILING_DATE, coalesce(H.coinsurere,'x') coinsurere,
                h.GIS_CHASSIS_NO, 
                h.bank,
                H.PLC_LOC_CODE AS BR_CODE,
                H.BRANCH AS BR_DESC,h.piy_desc,
                H.PDP_DEPT_CODE AS PDP_CODE,
                H.DEPARTMENT AS PDP_DESC,H.GIH_OLDCLAIM_NO,
                h.gih_doc_ref_no,h.gih_documentno, h.gih_year,
                H.INTIMATIONNO AS INTIMATIONNO,
                H.GIH_DATEOFLOSS AS DATEOFLOSS,
                H.POC_LOSSDESC AS LOSSDESC, h.psr_surv_name,
                H.GID_BASEDOCUMENTNO AS BASEDOCNO,H.PPS_PARTY_CODE,
                H.INSURED AS INSURED,H.REMARKS,H.PLACE,
                H.GID_COMMDATE AS COMMDATE, 
                coalesce( H.GIS_SUMINSURED,0) AS SUMINSURED, 
                coalesce (H.TOTLOSS,0) AS TOTALLOSS, 
                coalesce (H.LOSSPAY,0) AS LOSSPAY, 
                coalesce( H.SURYORPAY,0) AS SURVPAY, 
                coalesce (H.ADVOCATEPAY,0) AS ADVOCATEPAY,
                coalesce (SALVAGE,0) AS SALVAGE,
                COALESCE(H.TOTALPAID,0) TOTALPAID,
                H.GIH_INTIMATIONDATE AS INTIMATIONDATE,h.rev_date,
                H.GID_EXPIRYDATE AS EXPIRY  ,coalesce(h.gic_share,100)gic_share

                from (
                ( 
                select  
                IH.PLC_LOC_CODE        AS PLC_LOC_CODE, bnk.pbn_bnk_desc bank,
                LC.PLC_DESC            AS BRANCH, 
                DP.PDP_DESC            AS DEPARTMENT,    
                IH.GIH_INTIMATIONDATE  AS GIH_INTIMATIONDATE,   ih.gih_revisiondate rev_date,
                IH.PDP_DEPT_CODE       AS PDP_DEPT_CODE, IH.GIH_REMARKS REMARKS,
                IH.GIH_PLACEOFTHEFT PLACE,
                IH.POR_ORG_CODE        AS ORG,  ih.gih_year,
                IH.GIH_DOC_REF_NO,ih.gih_documentno,IH.GIH_OLDCLAIM_NO,
                IH.GIH_INTI_ENTRYNO    AS IntimationNo,  
                --iH.PIY_INSUTYPE,
                ID.GID_BASEDOCUMENTNO  AS GID_BASEDOCUMENTNO,  
                IH.GIH_DATEOFLOSS      AS GIH_DATEOFLOSS, sv.psr_surv_name,
                OC.POC_LOSSDESC        AS POC_LOSSDESC,   
                ID.GID_COMMDATE        AS GID_COMMDATE,  
                ID.GID_EXPIRYDATE      AS GID_EXPIRYDATE, coalesce(ic.gic_share,100)gic_share,
                piy.piy_desc,  ISUBDTL.GIS_REGISTRATION_NO,    
                ISUBDTL.GIS_ENGINE_NO,          
                ISUBDTL.GIS_CHASSIS_NO,  ISUBDTL.GIS_SAILING_DATE,
                IH.PPS_PARTY_CODE,       
                INS.PPS_DESC  AS INSURED, 
                coi.pps_desc coinsurere,
                SUM(coalesce(ISUBDTL.GIS_SUMINSURED,0))   AS GIS_SUMINSURED,  
                SUM(coalesce(IH.GIH_LOSSCLAIMED,0)) AS GSD_LOSSCLAIMED,  
                SUM(coalesce(ISUBDTL.GIS_SURVEYORAMT,0))  AS SURYORAMT,  
                SUM(coalesce(ISUBDTL.GIS_ADVOCATEAMT,0))  AS ADVORAMT,
 
                SUM(CASE coalesce(ISUBDTL.GIS_LOSSADJUSTED, 0)  WHEN 0 THEN  CASE coalesce(ISUBDTL.GIS_LOSSASSESSED, 0)  
                              WHEN 0 THEN  CASE coalesce(ISUBDTL.GIS_LOSSCLAIMED, 0)  WHEN 0 THEN 0  ELSE coalesce(ISUBDTL.GIS_LOSSCLAIMED, 0)  END  
                              ELSE coalesce (ISUBDTL.GIS_LOSSASSESSED, 0)  END  
                ELSE coalesce(ISUBDTL.gIS_LOSSADJUSTED, 0)  
                END * CASE  WHEN GIH_NOCLAIM_TAG = 'N' THEN 0 ELSE 1 END 
                + coalesce(ISUBDTL.GIS_SURVEYORAMT, 0) + coalesce(ISUBDTL.GIS_ADVOCATEAMT, 0) - 
                CASE 'Y' WHEN 'Y' THEN coalesce(ISUBDTL.GIS_SALVAGEAMT,0) ELSE 0 END
                ) as TotLoss,
                LOSSPAY * CASE WHEN GIH_NOCLAIM_TAG ='N' THEN 0 ELSE 1 END AS LOSSPAY,    
                SURYORPAY   AS SURYORPAY,    
                ADVOCATEPAY AS ADVOCATEPAY, 
                 (CASE 'Y' WHEN 'Y' THEN SALVAGE ELSE 0 END) AS SALVAGE,
                LOSSPAY + SURYORPAY  + ADVOCATEPAY - (CASE 'Y' WHEN 'Y' THEN SALVAGE ELSE 0 END) as TotalPaid  

                FROM       

                GI_GC_IH_INTIMATIONHD IH 

                left outer join GI_GC_ID_INTIMATIONDTL ID  on (  
                  IH.PDP_DEPT_CODE    =  ID.PDP_DEPT_CODE    AND      
                  IH.POR_ORG_CODE     =  ID.POR_ORG_CODE     AND  
                  IH.PLC_LOC_CODE     =  ID.PLC_LOC_CODE     AND        
                  IH.PDT_DOCTYPE      =  ID.PDT_DOCTYPE      AND         
                  IH.GIH_DOCUMENTNO   =  ID.GIH_DOCUMENTNO   AND      
                  IH.GIH_INTI_ENTRYNO =  ID.GIH_INTI_ENTRYNO AND      
                  IH.GIH_YEAR           =  ID.GIH_YEAR  )  

                left outer join GI_GC_IS_INTIMATIONSUBDTL ISUBDTL  on (  
                  ID.POR_ORG_CODE     = ISUBDTL.POR_ORG_CODE   AND             
                  ID.PLC_LOC_CODE     = ISUBDTL.PLC_LOC_CODE   AND              
                  ID.PDP_DEPT_CODE    = ISUBDTL.PDP_DEPT_CODE  AND            
                  ID.PDT_DOCTYPE      = ISUBDTL.PDT_DOCTYPE    AND               
                  ID.GIH_DOCUMENTNO   = ISUBDTL.GIH_DOCUMENTNO AND          
                  ID.GIH_INTI_ENTRYNO = ISUBDTL.GIH_INTI_ENTRYNO AND         
                  ID.GIH_YEAR         = ISUBDTL.GIH_YEAR       AND                     
                  ID.GID_PLY_SERIALNO = ISUBDTL.GID_PLY_SERIALNO )  
                left outer join GI_GC_IC_INTIMATION_COINSURER IC ON
                (
                ID.POR_ORG_CODE         = IC.POR_ORG_CODE     AND  
                ID.PLC_LOC_CODE         = IC.PLC_LOC_CODE     AND  
                ID.PDP_DEPT_CODE        = IC.PDP_DEPT_CODE    AND  
                ID.PDT_DOCTYPE          = IC.PDT_DOCTYPE         AND  
                ID.GIH_DOCUMENTNO       = IC.GIH_DOCUMENTNO   AND  
                ID.GIH_INTI_ENTRYNO     = IC.GIH_INTI_ENTRYNO AND  
                ID.GIH_YEAR             = IC.GIH_YEAR         AND  
                ID.GID_BASEDOCUMENTNO = IC.GIC_BASEDOCUMENTNO  AND 
                IC.GIC_CORETAG='C' AND IC.GIC_LEADERTAG='Y' 
                )
                left outer join pr_gn_ps_party coi on (coi.por_org_code=ih.por_org_code and coi.pps_party_code=ih.pps_insu_code)
                inner join  
                (select gih_doc_ref_no,max(gih_inti_entryno*1) ENTRYNO 
                from GI_GC_IH_INTIMATIONHD  where  --pdp_dept_code = '11'  and  
                case gih_inti_entryno when '1' then GIH_INTIMATIONDATE else GIH_REVISIONDATE end  BETWEEN   '01-Jan-2000'  AND '31-May-2018'
                and GIH_INTIMATIONDATE between '01-Jan-2000'  AND '31-May-2018'           
                group by gih_doc_ref_no) HD ON ( HD.gih_doc_ref_no=IH.gih_doc_ref_no AND ENTRYNO = IH.gih_inti_entryno ) 
 
                LEFT OUTER JOIN PR_GC_OC_LOSS_CAUSE OC ON (  
                IH.PDP_DEPT_CODE      = OC.PDP_DEPT_CODE       AND       
                IH.POC_LOSSCODE       = OC.POC_LOSSCODE   )  

                LEFT OUTER JOIN PR_GN_PS_PARTY INS ON     ( 
                IH.POR_ORG_CODE       =  INS.POR_ORG_CODE      AND  
                IH.PPS_PARTY_CODE   =  INS.PPS_PARTY_CODE )
 
                INNER JOIN PR_GN_DP_DEPARTMENT DP ON ( 
                IH.PLC_LOC_CODE  =  DP.PLC_LOC_CODE            AND  
                IH.PDP_DEPT_CODE = DP.PDP_DEPT_CODE  )
                left outer join GI_GC_UD_SURVEYORDTL svd on (
                IH.POR_ORG_CODE         = svd.POR_ORG_CODE     AND 
                IH.PLC_LOC_CODE          = svd.PLC_LOC_CODE     AND 
                IH.PDP_DEPT_CODE        = svd.PDP_DEPT_CODE    AND 
                IH.PDT_DOCTYPE            = svd.PDT_DOCTYPE      AND 
                IH.GIH_DOCUMENTNO     = svd.GIH_DOCUMENTNO   AND     
                IH.GIH_INTI_ENTRYNO    = svd.GIH_INTI_ENTRYNO AND 
                IH.GIH_YEAR                  = svd.GIH_YEAR and svd.gud_serialno=1) 
                left outer join PR_GG_SR_SURVEYOR sv on (
                svd.psr_surv_code=sv.psr_surv_code) 
                INNER JOIN PR_GN_LC_LOCATION LC ON  ( 
                IH.POR_ORG_CODE  =  LC.POR_ORG_CODE            AND  
                IH.PLC_LOC_CODE  =  LC.PLC_LOC_CODE )INNER JOIN
                PR_GG_IY_INSURANCETYPE PIY
                ON
                (
                iH.PIY_INSUTYPE=PIY.PIY_INSUTYPE
                )
                left outer join GI_GR_VC_VOYAGE_CARD vc 
                on
                (
                vc.GVC_POLICYNO = ID.GID_BASEDOCUMENTNO  and 
                vc.GVC_VOYAGECARD_NO = ID.GVC_VOYAGECARD_NO AND 
                vc.GVC_ITEMNO = ID.GID_ITEMNOS )
                INNER JOIN GI_GU_DH_DOC_HEADER DH ON (ID.GID_BASEDOCUMENTNO=DH.GDH_DOC_REFERENCE_NO AND DH.GDH_RECORD_TYPE='O')
                left outer join (
                select 
                BD.POR_ORG_CODE,BD.PLC_LOC_CODE,BD.PDP_DEPT_CODE,BD.PBC_BUSICLASS_CODE,BD.PIY_INSUTYPE,BD.PDT_DOCTYPE,   
                BD.GDH_DOCUMENTNO , BD.GDH_RECORD_TYPE,BD.GDH_YEAR,bn.PBN_BNK_DESC ,BD.PBN_BNK_CODE
                from GI_GU_BD_BANKDTL BD inner join PR_GN_BN_BANK BN on (
                BD.POR_ORG_CODE  =  BN.POR_ORG_CODE and
                BD.PBN_BNK_CODE  =  BN.PBN_BNK_CODE )
                ) bnk 
                on (
                dh.POR_ORG_CODE =  bnk.POR_ORG_CODE and
                dh.PLC_LOC_CODE  =  bnk.PLC_LOC_CODE   and
                dh.PDP_DEPT_CODE  =  bnk.PDP_DEPT_CODE   and
                dh.PBC_BUSICLASS_CODE =   bnk.PBC_BUSICLASS_CODE  and
                dh.PIY_INSUTYPE    =   bnk.PIY_INSUTYPE    and
                dh.PDT_DOCTYPE  =  bnk.PDT_DOCTYPE   and
                dh.GDH_DOCUMENTNO   =  bnk.GDH_DOCUMENTNO   and
                dh.GDH_RECORD_TYPE =  bnk.GDH_RECORD_TYPE and
                dh.GDH_YEAR    =    bnk.GDH_YEAR )
                LEFT OUTER JOIN (  
                select PD1.POR_ORG_CODE,PD1.PLC_LOC_CODE,PD1.PDP_DEPT_CODE,
                PD1.PDT_DOCTYPE,PD1.GIH_DOCUMENTNO,PD1.GIH_YEAR,PD1.GSD_SERIALNO,
                sum(LOSSPAY)     as LOSSPAY,
                sum(SURYORPAY)   as SURYORPAY,
                sum(ADVOCATEPAY) as ADVOCATEPAY, 
                sum(SALVAGE)     as SALVAGE 

                FROM         
                (select 
                SHD.POR_ORG_CODE,SHD.PLC_LOC_CODE,SHD.PDP_DEPT_CODE,SHD.PDT_DOCTYPE,
                SHD.GIH_DOCUMENTNO,SHD.GIH_INTI_ENTRYNO,SHD.GIH_YEAR,SHD.GSH_ENTRYNO, SDTL.GSD_SERIALNO,    
                CASE PYY_CODE WHEN '01' THEN (coalesce(GSD_LOSSPAID,0)) ELSE  0 END as LOSSPAY,        
                CASE PYY_CODE WHEN '02' THEN (coalesce(GSD_LOSSPAID,0)) ELSE  0 END as SURYORPAY,        
                CASE PYY_CODE WHEN '03' THEN (coalesce(GSD_LOSSPAID,0)) ELSE  0 END as ADVOCATEPAY, 
                CASE PYY_CODE WHEN '04' THEN (coalesce(GSD_LOSSPAID,0)) ELSE  0 END as SALVAGE,   
                coalesce(GSD_CONETAMOUNT,0)  net,
                coalesce(GSD_TREATYAMOUNT,0) treaty,
                coalesce(GSD_FACULTAMOUNT,0) fac,  
                coalesce(GSD_EXCESSAMOUNT,0) excess,
                coalesce(GSD_LOSSPAID,0)     PAID  
                FROM GI_GC_SH_SETTELMENTHD SHD 
                INNER JOIN GI_GC_SD_SETTELMENTDTL SDTL ON (            
                          SDTL.POR_ORG_CODE            = SHD.POR_ORG_CODE        AND       
                          SDTL.PLC_LOC_CODE            = SHD.PLC_LOC_CODE        AND       
                          SDTL.PDP_DEPT_CODE           = SHD.PDP_DEPT_CODE    AND       
                          SDTL.PDT_DOCTYPE             = SHD.PDT_DOCTYPE        AND       
                          SDTL.GIH_DOCUMENTNO          = SHD.GIH_DOCUMENTNO    AND       
                          SDTL.GIH_INTI_ENTRYNO        = SHD.GIH_INTI_ENTRYNO    AND       
                          SDTL.GIH_YEAR                = SHD.GIH_YEAR         AND      
                          SDTL.GSH_ENTRYNO             = SHD.GSH_ENTRYNO)
         
                WHERE SHD.GSH_SETTLEMENTDATE BETWEEN   SHD.GSH_SETTLEMENTDATE AND  '31-May-2018' 
                AND 
                coalesce(SHD.GSH_POSTINGTAG,'N') = 'Y') PD1  
                GROUP BY PD1.POR_ORG_CODE,PD1.PLC_LOC_CODE,PD1.PDP_DEPT_CODE,PD1.PDT_DOCTYPE,PD1.GIH_DOCUMENTNO,PD1.GIH_YEAR,PD1.GSD_SERIALNO 
                ) PD on ( 
                    PD.POR_ORG_CODE            = IH.POR_ORG_CODE    AND    
                    PD.PLC_LOC_CODE            = IH.PLC_LOC_CODE    AND    
                    PD.PDP_DEPT_CODE           = IH.PDP_DEPT_CODE    AND    
                    PD.PDT_DOCTYPE             = IH.PDT_DOCTYPE        AND    
                    PD.GIH_DOCUMENTNO          = IH.GIH_DOCUMENTNO    AND    
                    PD.GIH_YEAR                = IH.GIH_YEAR        AND  
                    PD.GSD_SERIALNO            = ID.GID_PLY_SERIALNO ) 
                where      
          
                dh.GDH_COMMDATE   between '01-Jan-2000'  AND '31-May-2018'
       
                and  COALESCE(IH.GIH_CANCELLATIONTAG,'N') =CASE 'N' WHEN 'Y' THEN 'Y' when 'N' then 'N' else COALESCE(IH.GIH_CANCELLATIONTAG,'N') END  
          
                Group by  
                IH.PLC_LOC_CODE,LC.PLC_DESC,DP.PDP_DESC,IH.GIH_INTIMATIONDATE,    ih.gih_revisiondate,piy.piy_desc,
                IH.PDP_DEPT_CODE,IH.POR_ORG_CODE,  IH.GIH_INTI_ENTRYNO , IH.GIH_DOC_REF_NO, IH.GIH_OLDCLAIM_NO,
                IH.GIH_NOCLAIM_TAG,ID.GID_BASEDOCUMENTNO,IH.GIH_DATEOFLOSS, OC.POC_LOSSDESC,ISUBDTL.GIS_REGISTRATION_NO,    
                ISUBDTL.GIS_ENGINE_NO,   ISUBDTL.GIS_SAILING_DATE, coi.pps_desc,       
                ISUBDTL.GIS_CHASSIS_NO, bnk.pbn_bnk_desc,
                ID.GID_COMMDATE,ID.GID_EXPIRYDATE,ih.PPS_PARTY_CODE,INS.PPS_DESC,SURYORPAY,ADVOCATEPAY,SALVAGE,LOSSPAY,ih.gih_documentno, ih.gih_year,IH.GIH_REMARKS,IH.GIH_PLACEOFTHEFT,coalesce(ic.gic_share,100), sv.psr_surv_name
       
                ) ) H 
                ORDER BY  H.PLC_LOC_CODE ,H.PDP_DEPT_CODE , SUBSTRING (H.INTIMATIONNO,1,4) ,SUBSTRING (H.INTIMATIONNO, LEN (H.INTIMATIONNO)-7,8) 
                )    WHERE (TOTALLOSS*GIC_SHARE/100)-(TOTALPAID*GIC_SHARE/100) <>0".Replace("31-May-2018", ToDate);

            
            db = new dal();
  

            if (!String.IsNullOrEmpty(IntimationNo)) {
                qry += " and gih_doc_ref_no ='" + IntimationNo + "'";

            }
            //piydesc,INTIMATIONNO,PPS_PARTY_CODE,INSURED,BR_CODE,BR_DESC
            if (!String.IsNullOrEmpty(brdescparam))
            {
                qry += " and BR_DESC ='" + brdescparam + "'";

            }

            if (!String.IsNullOrEmpty(insuredparam))
            {
                qry += " and INSURED ='" + insuredparam + "'";

            }
            if (!String.IsNullOrEmpty(piydesc))
            {
                qry += " and piydesc ='" + piydesc + "'";

            }

            qry += @"group by piydesc,INTIMATIONNO,PPS_PARTY_CODE,INSURED,BR_CODE,BR_DESC,PDP_CODE,PDP_DESC,gih_doc_ref_no, gih_year,GIC_SHARE 
                
                ORDER BY (TOTALLOSS*GIC_SHARE/100)-(TOTALPAID*GIC_SHARE/100)";



           




            db.getData(qry, dt);

        }

        public void getClaimHold(DataTable dt)
        {
            db = new dal();
            string qry = "select IDX,CLAIMHOLD from HICL_CLAIM_HOLD ";
            db.getData(qry, dt);
        }
        public void getClaimStatus(DataTable dt)
        {
            db = new dal();
            string qry = "select IDX,ClaimStatus from HICL_CLAIM_STATUS ";
            db.getData(qry, dt);
        }
        public void insertClaims(DataTable dt)
        {
            db = new dal();
            string qry = "insert into HICL_CLAIM_UPDATE (CLAIMIDX,USERIDX,DATETIME,DESCRIPTION,HOLDIDX,STATUSIDX) Values('" + claimIdx + "','" + userIdx + "','" + Date + "','" + desc + "','" + holdIdx + "','" + statusIdx + "')";

            db.getData(qry, dt);
        }
        public void getClaimDesc(DataTable dt,int claimIdx)
        {
            db = new dal();
            string qry = "select CLAIMIDX,USERIDX,DATETIME,DESCRIPTION,HOLDIDX,STATUSIDX,hcs.CLAIMSTATUS,hch.CLAIMHOLD from HICL_CLAIM_UPDATE hcu inner join HICL_CLAIM_STATUS hcs on hcs.IDX = STATUSIDX inner join HICL_CLAIM_HOLD hch on hch.IDX = HOLDIDX where claimidx ='"+ claimIdx + "' ";
            db.getData(qry, dt);
        }
    }
}