package com.ruoyi.product.service.impl;

import com.ruoyi.product.domain.MesTerminalSopHt;
import com.ruoyi.product.mapper.MesTerminalSopHtMapper;
import com.ruoyi.product.service.IMesTerminalSopHtService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class MesTerminalSopHtServiceImpl implements IMesTerminalSopHtService {

    @Autowired
    private MesTerminalSopHtMapper mesTerminalSopHtMapper;

    @Override
    public List<MesTerminalSopHt> selectMesTerminalSopHtList(MesTerminalSopHt mesTerminalSopHt) {
        return mesTerminalSopHtMapper.selectMesTerminalSopHtList(mesTerminalSopHt);
    }
}
